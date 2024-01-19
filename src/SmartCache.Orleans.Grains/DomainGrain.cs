using Orleans.Runtime;
using Orleans.Timers;
using SmartCache.Orleans.Grains.Interfaces;

namespace SmartCache.Orleans.Grains;
public class DomainGrain : Grain, IDomainGrain
{
    private readonly IPersistentState<State> _state;
    public IGrainContext GrainContext { get; }

    public DomainGrain(
        [PersistentState("State")] IPersistentState<State> state,
        ITimerRegistry timerRegistry,
        IGrainContext grainContext)
    {
        _state = state;
        timerRegistry.RegisterTimer(
            grainContext,
            asyncCallback: async state =>
            {
                await StoreState();
            },
            state: this,
            dueTime: TimeSpan.FromSeconds(300),
            period: TimeSpan.FromSeconds(300));

        GrainContext = grainContext;

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
        return true;
    }

    private async Task StoreState()
    {
        await _state.WriteStateAsync();
    }

    public class State
    {
        [Id(0)]
        public List<string> Emails { get; set; } = new();
    }
}
