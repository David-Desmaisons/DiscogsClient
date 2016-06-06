# DiscogsClient

C# Client library for [Discogs API v2.0](https://www.discogs.com/developers/)

## Features
* Include API to authorize user (generating OAuth1.0 token and token secret)
* Full support to [DataBase API](https://www.discogs.com/developers/#page:database) including image download
* Support of identity API
* Transparent support of rate limit
* Asynchroneous and cancellable API using Tasks
* Transparent management of pagination using none blocking API (Reactive IObservable) or IEnumerable

## Sample usage

####Create discogs client

```C#
  //Create authentication object using private and public keys: you should fournishes real keys here
  var oAuthCompleteInformation = new OAuthCompleteInformation("consumerKey", 
                                  "consumerSecret", "token", "tokenSecret");
  //Create discogs client using the authentication
  var discogsClient = new DiscogsClient(oAuthCompleteInformation);
```
####Search The DataBase

Using IObservable
```C#
  var discogsSearch = new DiscogsSearch()
  {
      artist = "Ornette Coleman",
      release_title = "The Shape Of Jazz To Come"
  };
    
  //Retrieve observable result from search
  var observable = _DiscogsClient.Search(discogsSearch);
```

Using IEnumerable
```C#
  //Alternatively retreive same result as enumerable 
  var enumerable = _DiscogsClient.SearchAsEnumerable(discogsSearch);
```

####Get Release, Master, Artist or Label Information
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

####Download Image
```C#
  //Retrieve Release information
  var res = await _DiscogsClient.GetMaster(47813);
  
  //Download the first image of the release
  await _DiscogsClient.SaveImage(res.images[0], Path.GetTempPath(), "Ornette-TSOAJTC");
```

See [DiscogsClientTest](https://github.com/David-Desmaisons/DiscogsClient/blob/master/DiscogsClient.Test/DiscogsClientTest.cs) for full sample of available APIs.
