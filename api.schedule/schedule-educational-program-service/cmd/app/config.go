package main

import (
	"fmt"
	"github.com/kelseyhightower/envconfig"
)

type PostgresqlConfig struct {
	Host     string `envconfig:"POSTGRESQL_HOST" required:"true"`
	Port     uint16 `envconfig:"POSTGRESQL_PORT" required:"true"`
	User     string `envconfig:"POSTGRESQL_USER" required:"true"`
	Password string `envconfig:"POSTGRESQL_PASSWORD" required:"true"`
	Database string `envconfig:"POSTGRESQL_DATABASE" required:"true"`
	SSLMode  bool   `envconfig:"POSTGRESQL_SSL_MODE" default:"false"`
}

type HttpConfig struct {
	Host string `envconfig:"HTTP_HOST" default:"localhost"`
	Port uint16 `envconfig:"HTTP_PORT" default:"8080"`
}

type GrpcConfig struct {
	Host string `envconfig:"GRPC_HOST" default:"localhost"`
	Port uint16 `envconfig:"GRPC_PORT" default:"9090"`
}

type Config struct {
	Postgres *PostgresqlConfig
	Http     *HttpConfig
	Grpc     *GrpcConfig
}

func newConfig() *Config {
	config := &Config{}
	err := envconfig.Process("", config)
	if err != nil {
		panic(err)
	}

	return config
}

func (c *PostgresqlConfig) URL() string {
	sslMode := func() string {
		if c.SSLMode {
			return "enable"
		}

		return "disable"
	}()

	return fmt.Sprintf("postgres://%s:%s@%s:%d/%s?sslmode=%s",
		c.User, c.Password, c.Host, c.Port, c.Database, sslMode)
}

func (c *HttpConfig) Address() string {
	return fmt.Sprintf("%s:%d", c.Host, c.Port)
}
