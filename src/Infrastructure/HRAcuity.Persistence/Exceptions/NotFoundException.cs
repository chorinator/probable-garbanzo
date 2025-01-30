namespace HRAcuity.Persistence.Exceptions;

public class NotFoundException(string notableQuoteNotFound) : Exception(notableQuoteNotFound);