package middleware

import (
	"errors"
	"net/http"
	"unsafe"
)

var errorCodes = map[ErrorCode]int{
	badRequest:   http.StatusBadRequest,
	unauthorized: http.StatusUnauthorized,
	notFound:     http.StatusNotFound,
}

type appHandler func(w http.ResponseWriter, r *http.Request) error

func ErrorMiddleware(h appHandler) http.HandlerFunc {
	return func(writer http.ResponseWriter, request *http.Request) {
		var appErr *AppError
		var msg string
		err := h(writer, request)
		if err != nil {
			if errors.As(err, &appErr) {
				writer.WriteHeader(errorCodes[appErr.Code])
				msg = appErr.Error()
			} else {
				writer.WriteHeader(http.StatusInternalServerError)
				msg = "Unknown error"
			}

			_, _ = writer.Write(*(*[]byte)(unsafe.Pointer(&msg)))
		}
	}
}
