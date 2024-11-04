﻿using System.Text;
using Anemoi.BuildingBlock.Application.Abstractions;
using Anemoi.BuildingBlock.Application.Cqrs.Queries.QueryFlow.QueryOneFlow;
using Anemoi.BuildingBlock.Application.Responses;
using Anemoi.BuildingBlock.Infrastructure.RequestHandlers.Queries.EntityFramework.EfQueryOne;
using Anemoi.Contract.Notification.Errors;
using Anemoi.Contract.Notification.Queries.EmailConfigurationQueries.GetEmailConfiguration;
using Anemoi.Contract.Notification.Responses;
using Anemoi.Contract.Secure.Queries.GetDecryptData;
using Anemoi.Contract.Secure.Responses;
using AutoMapper;
using MassTransit;
using Serilog;
using Anemoi.Notification.Domain.Models;
using OneOf;

namespace Anemoi.Notification.Application.Cqrs.Queries.EmailConfigurationQueries.GetEmailConfiguration;

public sealed class GetEmailConfigurationHandler(
    ISqlRepository<EmailConfiguration> sqlRepository,
    IMapper mapper,
    ILogger logger,
    IRequestClient<GetDecryptDataQuery> requestClient)
    : EfQueryOneHandler<EmailConfiguration, GetEmailConfigurationQuery, EmailConfigurationResponse>(sqlRepository,
        mapper, logger)
{
    protected override IQueryOneFlowBuilder<EmailConfiguration, EmailConfigurationResponse> BuildQueryFlow(
        IQueryOneFilter<EmailConfiguration, EmailConfigurationResponse> fromFlow, GetEmailConfigurationQuery query)
        => fromFlow
            .WithFilter(x => x.Id == query.Id)
            .WithSpecialAction(x => x)
            .WithErrorIfNull(NotificationErrorDetail.EmailConfigurationError.NotFound());

    protected override async Task<EmailConfigurationResponse> MapToResultAsync(
        GetEmailConfigurationQuery query,
        OneOf<EmailConfiguration, EmailConfigurationResponse> modelOrResponse)
    {
        var result = await base.MapToResultAsync(query, modelOrResponse);
        var passwordDecrypt = await requestClient
            .GetResponse<CryptographyResponse, ErrorDetailResponse>(new GetDecryptDataQuery(result.PasswordBytes));
        if (passwordDecrypt.Message is not CryptographyResponse data) return result;
        result.Password = Encoding.UTF8.GetString(data.DecryptData);
        return result;
    }
}