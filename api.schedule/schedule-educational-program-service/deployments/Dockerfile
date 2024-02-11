FROM golang:latest as build
LABEL authors="SV3TLuV"

WORKDIR /usr/src/app

COPY ../go.mod .
COPY go.sum .

RUN go mod download && go mod verify

COPY .. .

RUN go build -v

CMD ["app"]

FROM ubuntu:latest
LABEL authors="SV3TLuV"

COPY --from=build /usr/src/app /bin

ENTRYPOINT ["/bin"]