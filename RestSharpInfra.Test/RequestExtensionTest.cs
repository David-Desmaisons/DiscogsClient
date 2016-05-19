using RestSharp;
using System.ComponentModel;
using Xunit;
using NSubstitute;

namespace RestSharpInfra.Test
{
    public class RequestExtensionTest
    {
        private class Parameters
        {
            [Description("Parameter1")]
            public string P1 {get; set;}

            public string Parameter2 { get; set; }

            public int? Parameter3 { get; set; }
        };

        private readonly IRestRequest _RestRequest;
        private readonly Parameters _Parameters;

        public RequestExtensionTest()
        {
            _RestRequest = Substitute.For<IRestRequest>();
            _Parameters = GetParameter("p1value");
        }

        [Theory]
        [InlineData(null, "p2", null, "Parameter2", "p2")]
        [InlineData(null, null, 0, "Parameter3", 0)]
        public void AddAsParameter_CallAddParameters_UsingObjectAttribute(string p1, string p2, int? p3, string name, object value)
        {
            var param = GetParameter(p1, p2, p3);
            _RestRequest.AddAsParameter(param);

            _RestRequest.Received(1).AddParameter(name, value, Arg.Any<ParameterType>());
        }

        [Fact]
        public void AddAsParameter_CallAddParameters_UseParameterTypeQueryStringByDefault()
        {
            _RestRequest.AddAsParameter(_Parameters);

            _RestRequest.Received(1).AddParameter(Arg.Any<string>(), Arg.Any<string>(), ParameterType.QueryString);
        }

        [Fact]
        public void AddAsParameter_CallAddParameters_UseDescriptionForObjectAttribute()
        {
            _RestRequest.AddAsParameter(_Parameters);

            _RestRequest.Received(1).AddParameter("Parameter1", "p1value", Arg.Any<ParameterType>());
        }

        [Theory]
        [InlineData(ParameterType.HttpHeader)]
        [InlineData(ParameterType.RequestBody)]
        [InlineData(ParameterType.UrlSegment)]
        [InlineData(ParameterType.QueryString)]
        public void AddAsParameter_CallAddParameters_UsingParameterType(ParameterType type)
        {
            _RestRequest.AddAsParameter(_Parameters, type);

            _RestRequest.Received(1).AddParameter(Arg.Any<string>(), Arg.Any<string>(), type);
        }

        private Parameters GetParameter(string p1=null, string p2=null, int? p3=null)
        {
            return new Parameters()
            {
                P1 = p1,
                Parameter2 = p2,
                Parameter3 = p3
            };
        }
    }
}
