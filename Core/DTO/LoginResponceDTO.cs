namespace WebAPI.Core.DTO
{
    public class LoginResponceDTO
    {
            public bool IsAuthSuccessful { get; set; }
            public string ErrorMessage { get; set; }
            public string Token { get; set; }

    }
}
