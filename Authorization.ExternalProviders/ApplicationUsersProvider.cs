using Authorization.Application.Abstractions.ExternalProviders;
using Authorization.Application.Exceptions;
using Authorization.Application.Models;
using Authorization.ExternalProviders.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Authorization.ExternalProviders;

public class ApplicationUsersProvider : IApplicationUsersProviders
{


    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public ApplicationUsersProvider(IHttpClientFactory httpClientFactory,

        IConfiguration configuration)
    {

        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<GetApplicationUserDto?> GetApplicationUserAsync(string login, string password, CancellationToken cancellationToken)
    {

        var userServiceUrl = _configuration["ApplicationUserServiceApiUrl"];
        //var getUserApiMethodUrl = $"{userServiceUrl}ApplicationUser?login={login}&password={password}";
        var getUserApiMethodUrl = $"{userServiceUrl}ApplicationUser";
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, getUserApiMethodUrl);
        httpRequest.Content = JsonContent.Create(new { Login = login, Password = password });
        var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {

            var serviceName = "ManageUsersService";
            var requestUrlMessage = $"request url '{getUserApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }
            // TODO check status Code getUserApiMethodUrl
            if (responseMessage.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ForbiddenException();
            }

            return null;

        }

        var jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var getUserDto = JsonSerializer.Deserialize<GetApplicationUserDto>(jsonResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });



        return new GetApplicationUserDto(
            getUserDto!.ApplicationUserId,
            getUserDto.ApplicationUserRole,
            getUserDto.Login,
            getUserDto.PasswordHash);

    }

    public async Task<GetApplicationUserDto> GetApplicationUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userServiceUrl = _configuration["ApplicationUserServiceApiUrl"];
        var getUserApiMethodUrl = $"{userServiceUrl}ApplicationUser/{id}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, getUserApiMethodUrl);
        var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
        if (!responseMessage.IsSuccessStatusCode)
        {
            var serviceName = "ManageUsersService";
            var requestUrlMessage = $"request url '{getUserApiMethodUrl}'";
            if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
            }

            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }

        var jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var getUserDto = JsonSerializer.Deserialize<GetApplicationUserDto>(jsonResult, new JsonSerializerOptions
        {

            PropertyNameCaseInsensitive = true
        });


        return new GetApplicationUserDto(getUserDto!.ApplicationUserId, getUserDto.ApplicationUserRole, getUserDto.Login, getUserDto.PasswordHash);
    }
}