﻿using RentCarApi.Application.Features.Response;

namespace RentCarApi.Application.Features.Commands.Order.Create;
public class OrderCreateCommandResponse
{
    public bool Succeeded { get; set; } = true;
    public string Message { get; set; } = ResponseMessages.Success;
}
