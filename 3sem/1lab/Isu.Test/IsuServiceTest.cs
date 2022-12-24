using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;

using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private readonly IsuService _isuService;

    public IsuServiceTest()
    {
        _isuService = new IsuService();
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        Group group = _isuService.AddGroup(new GroupName("M3101"));

        Student student = _isuService.AddStudent(group, StudentName.Parse("Ivanov Ivan"));

        Assert.Equal(student.Group, group);
        Assert.Contains(student, group.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        Group group = _isuService.AddGroup(new GroupName("M3101"));

        for (int i = 0; i < Group.MaxStudentsCount; ++i)
            _isuService.AddStudent(group, StudentName.Parse("Ivanov Ivan"));

        Assert.Throws<InvalidGroupOperationException>(()
            => _isuService.AddStudent(group, StudentName.Parse("Ivanov Ivan")));
    }

    [Fact]
    public void CreateExistingGroup_ThrowException()
    {
        var name = new GroupName("M3101");
        _isuService.AddGroup(name);
        Assert.ThrowsAny<InvalidIsuOperationException>(() => _isuService.AddGroup(name));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group group1 = _isuService.AddGroup(new GroupName("M3100"));
        Group group2 = _isuService.AddGroup(new GroupName("M3101"));
        Student student = _isuService.AddStudent(group1, StudentName.Parse("Ivanov Ivan"));

        _isuService.ChangeStudentGroup(student, group2);

        Assert.Equal(student.Group, group2);
        Assert.Contains(student, group2.Students);
        Assert.DoesNotContain(student, group1.Students);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("Abcd")]
    [InlineData("A b c d")]
    public void CreateStudentWithInvalidName_ThrowException(string name)
    {
        Group group = _isuService.AddGroup(new GroupName("M3101"));
        Assert.Throws<InvalidStudentNameException>(() => _isuService.AddStudent(group, StudentName.Parse(name)));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("Abcd")]
    [InlineData("1234")]
    [InlineData("M000")]
    [InlineData("M3900")]
    [InlineData("M319999")]
    public void CreateGroupWithInvalidName_ThrowException(string name)
    {
        Assert.ThrowsAny<IsuDomainException>(() => _isuService.AddGroup(new GroupName(name)));
    }

    [Theory]
    [InlineData("M3101")]
    [InlineData("P34152")]
    [InlineData("R42151")]
    public void CreateGroupWithValidName_NameParsedCorrectly(string name)
    {
        Group group = _isuService.AddGroup(new GroupName(name));
        Assert.Equal(name, group.Name.ToString());
    }

    [Theory]
    [InlineData("Ivanov Ivan")]
    [InlineData("Ivanov Ivan Ivanovich")]
    public void CreateStudentWithValidName_NameParsedCorrectly(string name)
    {
        Group group = _isuService.AddGroup(new GroupName("M3101"));
        Student student = _isuService.AddStudent(group, StudentName.Parse(name));
        Assert.Equal(name, student.FullName.ToString());
    }
}