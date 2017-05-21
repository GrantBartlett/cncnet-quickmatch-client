using System;
using System.Net;

public class CnCNetEventFatalError : EventArgs
{
    public string ErrorMessage { get; private set; }
    public string ErrorCode { get; private set; }
    private string defaultMessage = "Look's like we've got a problem. Please check https://cncnet.org/status for more information";

    public CnCNetEventFatalError(HttpStatusCode errorCode, string errorMessage = null)
    {
        ErrorMessage = errorMessage != null ? errorMessage : defaultMessage;
        ErrorCode = errorCode.ToString();
    }
}
