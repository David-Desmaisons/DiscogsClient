# DiscogsClient

[![Build status](https://img.shields.io/appveyor/ci/David-Desmaisons/DiscogsClient.svg?maxAge=592000)](https://ci.appveyor.com/project/David-Desmaisons/DiscogsClient)
[![NuGet Badge](https://img.shields.io/nuget/v/DiscogsClient.svg)](https://www.nuget.org/packages/DiscogsClient/)
[![MIT License](https://img.shields.io/github/license/David-Desmaisons/DiscogsClient.svg)](https://github.com/David-Desmaisons/DiscogsClient/blob/master/LICENSE)


C# Client library for [Discogs API v2.0](https://www.discogs.com/developers/)

Check demo application [Music.Cover.Finder](https://github.com/David-Desmaisons/Music.Cover.Finder)

## Features
* Include API to authorize user (generating OAuth1.0 token and token secret)
* Full support to [DataBase API](https://www.discogs.com/developers/#page:database) including image download
* Support of identity API
* Transparent support of rate limit
* Asynchroneous and cancellable API using Tasks
* Transparent management of pagination using none blocking API (Reactive IObservable) or IEnumerable

## Sample usage

### Create discogs client

* Oauth authentication
```C#
  //Create authentication object using private and public keys: you should fournish real keys here
  var oAuthCompleteInformation = new OAuthCompleteInformation("consumerKey", 
                                  "consumerSecret", "token", "tokenSecret");
  //Create discogs client using the authentication
  var discogsClient = new DiscogsClient(oAuthCompleteInformation);
```
* Token based authentication
```C#
  //Create authentication based on Discogs token
  var tokenInformation = new TokenAuthenticationInformation("my-token");
  //Create discogs client using the authentication
  var discogsClient = new DiscogsClient(tokenInformation);
```
#### Search The DataBase

Using IObservable:
```C#
var discogsSearch = new DiscogsSearch()
{
  artist = "Ornette Coleman",
  release_title = "The Shape Of Jazz To Come"
};
    
//Retrieve observable result from search
var observable = _DiscogsClient.Search(discogsSearch);
```

Using IEnumerable:
```C#
//Alternatively retreive same result as enumerable 
var enumerable = _DiscogsClient.SearchAsEnumerable(discogsSearch);
```

#### Get Release, Master, Artist or Label Information
```C#
var release = await _DiscogsClient.GetReleaseAsync(1704673);
```

```C#
var master = await _DiscogsClient.GetMasterAsync(47813);
```

```C#
var artist = await _DiscogsClient.GetArtistAsync(224506);
```

```C#
var label = await _DiscogsClient.GetLabelAsync(125);
```

#### Download Image
```C#
//Retrieve Release information
var res = await _DiscogsClient.GetMasterAsync(47813);
  
//Download the first image of the release
await _DiscogsClient.SaveImageAsync(res.images[0], Path.GetTempPath(), "Ornette-TSOAJTC");
```

#### OAuth: Authorize new user
```C#
//Create authentificator information: you should fournish real keys here
var oAuthConsumerInformation = new OAuthConsumerInformation("consumerKey", "consumerSecret");
  
//Create Authentifier client
var discogsAuthentifierClient = new DiscogsAuthentifierClient(oAuthConsumerInformation);

//Retreive Token and Token secret 
var oauth = discogsClient.Authorize(s => Task.FromResult(GetToken(s))).Result;
```

Authorize takes a Func< string, Task< string>> as parameter, receiving the authentication url and returning the corresponding access key. Trivial implementation:

```C#
private static string GetToken(string url)
{
  Console.WriteLine("Please authorize the application and enter the final key in the console");
  Process.Start(url);
  return Console.ReadLine();
}
```
See [DiscogsClientTest](https://github.com/David-Desmaisons/DiscogsClient/blob/master/DiscogsClient.Test/DiscogsClientTest.cs) and [DiscogsAuthenticationConsole](https://github.com/David-Desmaisons/DiscogsClient/blob/master/DiscogsAuthenticationConsole/Program.cs) for full samples of available APIs.
