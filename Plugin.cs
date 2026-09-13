using LabApi.Events.CustomHandlers;
using LabApi.Loader.Features.Plugins;
using System;
using System.Collections.Generic;
using Voting.Core.Handlers;

namespace Voting.Core;

public class Plugin : Plugin<Config>
{
    private HashSet<CustomEventsHandler> _handlers;

    public override string Name { get; } = "Voting.Core";
    public override string Author => "IOT_TOI";
    public override string Description => string.Empty;
    public override Version RequiredApiVersion => LabApi.Features.LabApiProperties.CurrentVersion;

    public static Plugin Instance { get; private set; }

    public override void Enable()
    {
        Instance = this;
        RegisterHandlers();
    }

    public override void Disable()
    {
        UnregisterHandlers();
        Instance = null;
    }

    private void RegisterHandlers()
    {
        _handlers = [
            new ServerHandler()
        ];

        ProcessHandlers(CustomHandlersManager.RegisterEventsHandler);
    }

    private void UnregisterHandlers() => ProcessHandlers(CustomHandlersManager.UnregisterEventsHandler);
    private void ProcessHandlers(Action<CustomEventsHandler> action)
    {
        foreach (var handler in _handlers)
        {
            action(handler);
        }
    }
}
