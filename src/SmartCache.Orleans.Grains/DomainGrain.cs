using Orleans.Runtime;
using SmartCache.Orleans.Grains.Interfaces;

namespace SmartCache.Orleans.Silo.Grains;
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

    public async Task AddBreachedEmailAsync(string email)
    {
        _state.State.Emails.Add(email);
        await _state.WriteStateAsync();
    }

    public class State
    {
        [Id(0)]
        public List<string> Emails { get; set; } = new();
    }
}
