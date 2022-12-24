using Shops.Models;

namespace Shops.Builders;

public class CustomerRequestsBuilder
{
    private List<CustomerRequest> _requests;

    public CustomerRequestsBuilder()
    {
        _requests = new List<CustomerRequest>();
    }

    public CustomerRequestsBuilder Reset()
    {
        _requests = new List<CustomerRequest>();

        return this;
    }

    public CustomerRequestsBuilder AddRequest(CustomerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        _requests.Add(request);

        return this;
    }

    public IEnumerable<CustomerRequest> GetResult()
    {
        IEnumerable<CustomerRequest> result = _requests;

        Reset();

        return result;
    }
}
