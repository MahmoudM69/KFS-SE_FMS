using Microsoft.AspNetCore.Components;
using System;
using System.Threading;

namespace Server.Shared;

public abstract class CancellableComponent : ComponentBase, IDisposable
{
    private readonly CancellationTokenSource _cts = new();
    protected CancellationToken CancellationToken => _cts.Token;

    void IDisposable.Dispose()
    {
        if (_cts != null)
        {
            _cts.Cancel();
            _cts.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
