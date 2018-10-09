using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System.Collections.Generic;

namespace Huach.Framework.Jwt
{
    public class JwtHelper
    {
        public static string Encode(object payload, string secret)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            return new JwtEncoder(algorithm, serializer, urlEncoder).Encode(payload, secret);
        }
        public static JwtDecode<T> Decode<T>(string token, string secret)
        {
            JwtDecode<T> jwtDecodeInfo = new JwtDecode<T>
            {
                VerifyResult = JwtVerifyResult.Fail
            };
            try
            {
                var jns = new JsonNetSerializer();
                IDateTimeProvider dateTimeProvider = new UtcDateTimeProvider();
                IJwtValidator jwtValidator = new JwtValidator(jns, dateTimeProvider);
                IBase64UrlEncoder base64UrlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder jwtDecoder = new JwtDecoder(jns, jwtValidator, base64UrlEncoder);
                jwtDecodeInfo.Payload = jwtDecoder.DecodeToObject<T>(token, secret, true);
                jwtDecodeInfo.VerifyResult = JwtVerifyResult.Succeed;
            }
            catch (TokenExpiredException)
            {
                jwtDecodeInfo.VerifyResult = JwtVerifyResult.Expired;
                jwtDecodeInfo.Msg = "Token已过期";
            }
            catch (SignatureVerificationException)
            {
                jwtDecodeInfo.VerifyResult = JwtVerifyResult.InvalidSignature;
                jwtDecodeInfo.Msg = "Token签名无效";
            }
            return jwtDecodeInfo;
        }
        public static JwtDecode<Dictionary<string, object>> Decode(string token, string secret)
        {
            return Decode<Dictionary<string, object>>(token, secret);
        }

    }
}
