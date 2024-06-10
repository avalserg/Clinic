using Microsoft.Extensions.Configuration;
using PatientTickets.Application.Abstractions.ExternalProviders;
using PatientTickets.Application.DTOs.ExternalProviders;
using PatientTickets.Domain.Exceptions.Base;
using PatientTickets.ExternalProviders.Exceptions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace PatientTickets.ExternalProviders;

public class ManageUsersHttpProvider : IManageUsersProviders
{


    private readonly IConfiguration _configuration;

    private readonly HttpClient _httpClient;

    public ManageUsersHttpProvider(IHttpClientFactory httpClientFactory,

        IConfiguration configuration)
    {

        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<GetDoctorDto> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken)
    {

        var userServiceUrl = _configuration["ManageUsersServiceApiUrl"];
        var getUserApiMethodUrl = $"{userServiceUrl}Doctors/{id}";
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, getUserApiMethodUrl);
        httpRequest.Content = JsonContent.Create(new { Id = id });
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
            throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
        }

        var jsonResult = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var getUserDto = JsonSerializer.Deserialize<GetDoctorDto>(jsonResult, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return new GetDoctorDto(getUserDto!.FirstName, getUserDto.LastName, getUserDto.Patronymic, getUserDto.CabinetNumber, getUserDto.Speciality);
    }


}