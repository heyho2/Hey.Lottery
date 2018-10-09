namespace Huach.Framework.Jwt
{
    public class JwtDecode<T>
    {
        public virtual bool IsSucceed => VerifyResult == JwtVerifyResult.Succeed;
        public JwtVerifyResult VerifyResult { get; set; }
        public string Msg { get; set; }
        public T Payload { get; set; }
    }

    public enum JwtVerifyResult
    {
        Fail = 1,
        Succeed = 2,
        Expired = 3,
        InvalidSignature = 4
    }
    public enum JwtClaimName
    {
        iss,
        sub,
        aud,
        exp,
        nbf,
        iat,
        jti,
        preferred_username,
        phone_number,
        auth_time
    }
}
