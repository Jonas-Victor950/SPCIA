using System.Globalization;

namespace BRCSystem.ClassLibrary.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base() { }

        public UnauthorizedException(string message) : base(message) { }

        public UnauthorizedException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base() { }

        public ForbiddenException(string message) : base(message) { }

        public ForbiddenException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class NoContentException : Exception
    {
        public NoContentException() : base() { }
        public NoContentException(string message) : base(message) { }
        public NoContentException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class ConflictException : Exception
    {
        public ConflictException() : base() { }
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class MethodNotAllowed : Exception
    {
        public MethodNotAllowed() : base() { }
        public MethodNotAllowed(string message) : base(message) { }
        public MethodNotAllowed(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class BadGateway : Exception
    {
        public BadGateway() : base() { }
        public BadGateway(string message) : base(message) { }
        public BadGateway(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class DoneStatusCode : Exception
    {
        public DoneStatusCode() : base() { }
        public DoneStatusCode(string message) : base(message) { }
        public DoneStatusCode(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class CancelStatusCode : Exception
    {
        public CancelStatusCode() : base() { }
        public CancelStatusCode(string message) : base(message) { }
        public CancelStatusCode(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class UserUnauthorized : Exception
    {
        public UserUnauthorized() : base() { }
        public UserUnauthorized(string message) : base(message) { }
        public UserUnauthorized(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class BlockedException : Exception
    {
        public BlockedException() : base() { }
        public BlockedException(string message) : base(message) { }
        public BlockedException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    /// <summary>Status 1005</summary>
    public class LotStepDataResetBlockedException : Exception
    {
        public LotStepDataResetBlockedException() : base() { }
        public LotStepDataResetBlockedException(string message) : base(message) { }
        public LotStepDataResetBlockedException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    /// <summary>Status 1006</summary>
    public class ProductTypePrefixListException : Exception
    {
        public ProductTypePrefixListException() : base() { }
        public ProductTypePrefixListException(string message) : base(message) { }
        public ProductTypePrefixListException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    /// <summary>Status 1007</summary>
    public class ProcessStepTimeInvalid : Exception
    {
        public ProcessStepTimeInvalid() : base() { }
        public ProcessStepTimeInvalid(string message) : base(message) { }
        public ProcessStepTimeInvalid(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    /// <summary>Status 1008</summary>
    public class PrintLabelNotFoundOrAvailable : Exception
    {
        public PrintLabelNotFoundOrAvailable() : base() { }
        public PrintLabelNotFoundOrAvailable(string message) : base(message) { }
        public PrintLabelNotFoundOrAvailable(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    /// <summary>Status 1009</summary>
    public class PrintLabelQtyNotFound : Exception
    {
        public PrintLabelQtyNotFound() : base() { }
        public PrintLabelQtyNotFound(string message) : base(message) { }
        public PrintLabelQtyNotFound(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    /// <summary>Status 1010</summary>
    public class InvalidPassWord : Exception
    {
        public InvalidPassWord() : base() { }
        public InvalidPassWord(string message) : base(message) { }
        public InvalidPassWord(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    /// <summary>Status 1011</summary>
    public class ResultIsEmpty : Exception
    {
        public ResultIsEmpty() : base() { }
        public ResultIsEmpty(string message) : base(message) { }
        public ResultIsEmpty(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    /// <summary>Status 1012</summary>
    public class CannotDelete : Exception
    {
        public CannotDelete() : base() { }
        public CannotDelete(string message) : base(message) { }
        public CannotDelete(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    public class CannotUpdate : Exception
    {
        public CannotUpdate() : base() { }
        public CannotUpdate(string message) : base(message) { }
        public CannotUpdate(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }

    public class ProductDesable : Exception
    {
        public ProductDesable() : base() { }
        public ProductDesable(string message) : base(message) { }
        public ProductDesable(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args)) { }
    }
}
