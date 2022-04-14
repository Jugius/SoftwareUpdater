﻿using SoftwareUpdater.ApiSoftware.Entities;
using SoftwareUpdater.Interfaces;
using SoftwareUpdater.ApiSoftware;

namespace SoftwareUpdater;

public abstract class UpdaterBase
{
    protected static bool IsDevelopmentVersion(Version version) => version.Revision > 0;
    public IUpdatableApplication Application { get; }
    public UpdaterBase(IUpdatableApplication application) => this.Application = application;
    internal async Task<Release[]> GetReleasesAsync()
    {
        var request = new NewestReleasesRequest
        {
            ApplicationId = this.Application.ApplicationID,
            Version = this.Application.CurrentVersion.ToString()
        };
        try
        {
            var response = await HttpEngine.QueryAsync(request).ConfigureAwait(false);
            if (response.Status == "Ok")
            {
                return response.Releases ?? Array.Empty<Release>();
            }
            else
            {
                throw new Exception(response.Status);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

}
