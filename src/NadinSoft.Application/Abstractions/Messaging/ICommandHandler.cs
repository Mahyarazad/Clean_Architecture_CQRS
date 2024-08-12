﻿using FluentResults;
using MediatR;

namespace NadinSoft.Application.Abstractions.Messaging
{
    internal interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand: ICommand<TResponse>
    {
    }
}
