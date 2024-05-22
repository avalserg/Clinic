using System.Net;
using System.Text.Json;
using Authorization.Application.Abstractions.ExternalProviders;
using Authorization.Application.Abstractions.Persistence.Repository.Read;
using Authorization.Application.Abstractions.Persistence.Repository.Writing;
using Authorization.Application.Models;
using Authorization.ExternalProviders.Exceptions;
using Microsoft.Extensions.Configuration;

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
    
    public async Task<GetApplicationUserDto> GetApplicationUserAsync(Guid ownerId, CancellationToken cancellationToken)
    {
       
        var userServiceUrl = _configuration["ApplicationUserServiceApiUrl"];
        var getUserApiMethodUrl = $"{userServiceUrl}ApplicationUser/{ownerId.ToString()}";
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
        
       
        return new GetApplicationUserDto(getUserDto!.ApplicationUserId, getUserDto.ApplicationUserRole, getUserDto.Login);
       
    }

    
}