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
        Group group = _isuService.AddGroup(new GroupName("M3456"));

        Student student = _isuService.AddStudent(group, "Test student");

        Assert.Equal(student.Group, group);
        Assert.Contains(student, group.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        Group testGroup = _isuService.AddGroup(new GroupName("M3456"));

        for (int i = 0; i < Group.MaxStudentsCount; ++i)
            _isuService.AddStudent(testGroup, $"Test student {i}");

        Assert.Throws<GroupOperationException>(()
            => _isuService.AddStudent(testGroup, $"Test student {Group.MaxStudentsCount}"));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.ThrowsAny<IsuDomainException>(() => new GroupName("ABCD"));
    }

    [Fact]
    public void CreateStudentWithInvalidName_ThrowException()
    {
        Assert.Throws<InvalidStudentNameException>(() => new StudentName("ABCD"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group group1 = _isuService.AddGroup(new GroupName("M3101"));
        Group group2 = _isuService.AddGroup(new GroupName("M3102"));
        Student student = _isuService.AddStudent(group1, "Test student");

        _isuService.ChangeStudentGroup(student, group2);

        Assert.Equal(student.Group, group2);
        Assert.Contains(student, group2.Students);
        Assert.DoesNotContain(student, group1.Students);
    }
}