using Orleans.Runtime;
using SmartCache.Orleans.Grains.Interfaces;

namespace SmartCache.Orleans.Grains;
public class DomainGrain : Grain, IDomainGrain
{
    private readonly IPersistentState<State> _state;

    public DomainGrain([PersistentState("State")] IPersistentState<State> state)
    {
        _state = state;
    }

    public Task<bool> IsEmailBreachedAsync(string email)
    {
        return Task.FromResult(_state.State.Emails.Contains(email));
    }

    public async Task<bool> AddBreachedEmailAsync(string email)
    {
        if (await IsEmailBreachedAsync(email))
        {
            return false;
        }

        _state.State.Emails.Add(email);
        await _state.WriteStateAsync();
        return true;
    }

    public class State
    {
        [Id(0)]
        public List<string> Emails { get; set; } = new();
    }
}
