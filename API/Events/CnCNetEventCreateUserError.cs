using System;
using System.Net;

public class CnCNetEventCreateUserError : EventArgs
{
    public string ErrorMessage { get; private set; }
    public string ErrorCode { get; private set; }

    public CnCNetEventCreateUserError(string errorMessage, HttpStatusCode errorCode)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode.ToString();
    }
}
