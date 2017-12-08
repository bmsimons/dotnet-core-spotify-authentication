# dotnet-core-spotify-authentication

A web API app built on .NET Core to show how the Spotify authentication flow works.
Almost identical to the 'authorization_code' example from the official [Spotify repo](https://github.com/spotify/web-api-auth-examples), which is written in Node.

Want to learn more about Spotify app authentication flow? Please check [this page of their documentation](https://developer.spotify.com/web-api/authorization-guide/).

# Prerequisites

You just need to have the .NET Core SDK installed. And you need a Spotify account :)

# Installation guide

```
git clone https://github.com/bmsimons/dotnet-core-spotify-authentication.git
cd dotnet-core-spotify-authentication
dotnet run
```

# Configuration guide

Don't forget to replace the client ID and client secret in SpotifyController.cs.
Oh, and please add the callback URL to the whitelist of your app. You can do that on your [Spotify app dashboard](https://beta.developer.spotify.com/dashboard/applications).

# Any questions?

Feel free to contact me :)


