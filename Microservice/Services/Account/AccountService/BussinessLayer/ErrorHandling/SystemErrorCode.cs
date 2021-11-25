namespace AccountService.BusinessLayer.ErrorHandling
{
    public enum SystemErrorCode
    {
        SystemError = 0,
        ValidationError,
        EntityNotFound,
        CreditsMissing
    }
}