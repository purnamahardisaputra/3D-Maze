using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.RemoteConfig;
using UnityEngine;

public struct userAttributes
{
    public int characterLevels;
};
public struct appAttributes
{

};

public class RemoteConfigFetcher : MonoBehaviour
{
    [SerializeField] string environmentName;
    [SerializeField] int characterLevels;
    [SerializeField] bool fetch;
    [SerializeField] float gravity;
    [SerializeField] PhoneGravity phoneGravity;
    // Start is called before the first frame update
    async void Awake()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName(environmentName);
        await UnityServices.InitializeAsync(options);

        Debug.Log("UGS Initialized");

        if (AuthenticationService.Instance.IsSignedIn == false)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Debug.Log("UGS SIgned In");

        RemoteConfigService.Instance.FetchCompleted += OnFetchCompleted;
    }

    private void OnDestroy()
    {
        RemoteConfigService.Instance.FetchCompleted -= OnFetchCompleted;
    }

    private void OnFetchCompleted(ConfigResponse response)
    {
        Debug.Log(response.requestOrigin);
        Debug.Log(response.body);

        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("Default");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("Cached");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("Remote");
                gravity = RemoteConfigService.Instance.appConfig.GetFloat("Gravity");
                phoneGravity.SetGravityMagnitude(gravity);
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fetch)
        {
            fetch = false;
            Debug.Log("Fetching Remote Config");
            RemoteConfigService.Instance.FetchConfigs(new userAttributes() { characterLevels = this.characterLevels }, new appAttributes());
        }
    }
}
