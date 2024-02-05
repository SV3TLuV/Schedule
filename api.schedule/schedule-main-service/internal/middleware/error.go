package middleware

import "encoding/json"

const (
	notFound     ErrorCode = "NOT_FOUND"
	unauthorized ErrorCode = "UNAUTHORIZED"
	badRequest   ErrorCode = "BAD_REQUEST"
)

type ErrorCode string

type AppError struct {
	Err        error  `json:"-"`
	Message    string `json:"message"`
	DevMessage string `json:"dev_message"`
	Code       ErrorCode
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

func NewAppError(msg, devMsg string, err error) *AppError {
	return &AppError{
		Err:        err,
		Message:    msg,
		DevMessage: devMsg,
	}
}
