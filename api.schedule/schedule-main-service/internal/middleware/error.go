package middleware

import "encoding/json"

const (
	badRequest   ErrorCode = "BAD_REQUEST"
	unauthorized ErrorCode = "UNAUTHORIZED"
	notFound     ErrorCode = "NOT_FOUND"
)

var (
	BadRequest   = NewAppError("Bad request.", badRequest, nil)
	Unauthorized = NewAppError("Unauthorized.", unauthorized, nil)
	NotFound     = NewAppError("Not found.", notFound, nil)
)

type ErrorCode string

type AppError struct {
	Err     error  `json:"-"`
	Message string `json:"message"`
	Code    ErrorCode
}

func (e *AppError) Error() string {
	message := e.Message
	if e.Err != nil {
		message = e.Err.Error()
	}
	return message
}

func (e *AppError) Unwrap() error {
	return e.Err
}

func (e *AppError) Marshal() []byte {
	marshal, err := json.Marshal(e)
	if err != nil {
		return nil
	}
	return marshal
}

func NewAppError(msg string, code ErrorCode, err error) *AppError {
	return &AppError{
		Err:     err,
		Message: msg,
		Code:    code,
	}
}
