# DiscogsClient

C# Client library for [Discogs API v2.0](https://www.discogs.com/developers/)

Nuget [here](https://www.nuget.org/packages/DiscogsClient/)

## Features
* Include API to authorize user (generating OAuth1.0 token and token secret)
* Full support to [DataBase API](https://www.discogs.com/developers/#page:database) including image download
* Support of identity API
* Transparent support of rate limit
* Asynchroneous and cancellable API using Tasks
* Transparent management of pagination using none blocking API (Reactive IObservable) or IEnumerable

## Sample usage

#### Create discogs client

```C#
  //Create authentication object using private and public keys: you should fournish real keys here
  var oAuthCompleteInformation = new OAuthCompleteInformation("consumerKey", 
                                  "consumerSecret", "token", "tokenSecret");
  //Create discogs client using the authentication
  var discogsClient = new DiscogsClient(oAuthCompleteInformation);
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
  var release = await _DiscogsClient.GetRelease(1704673);
```

```C#
  var master = await _DiscogsClient.GetMaster(47813);
```

```C#
  var artist = await _DiscogsClient.GetArtist(224506);
```

```C#
  var label = await _DiscogsClient.GetLabel(125);
```

#### Download Image
```C#
  //Retrieve Release information
  var res = await _DiscogsClient.GetMaster(47813);
  
  //Download the first image of the release
  await _DiscogsClient.SaveImage(res.images[0], Path.GetTempPath(), "Ornette-TSOAJTC");
```

#### Authorize new user
```C#
  //Create authentificator information: you should fournish real keys here
  var oAuthConsumerInformation = new OAuthConsumerInformation("consumerKey", "consumerSecret");
  
  //Create Authentifier client
  var discogsAuthentifierClient = new DiscogsAuthentifierClient(oAuthConsumerInformation);

  //Retreive Token and Token secret 
  var aouth = discogsClient.Authorize(s => Task.FromResult(GetToken(s))).Result;
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
