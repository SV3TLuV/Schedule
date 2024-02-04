package config

import (
	"errors"
	"fmt"
	"os"
	"strconv"
)

const (
	postgresqlHostEnvName     = "POSTGRESQL_HOST"
	postgresqlPortEnvName     = "POSTGRESQL_PORT"
	postgresqlUserEnvName     = "POSTGRESQL_USER"
	postgresqlPasswordEnvName = "POSTGRESQL_PASSWORD"
	postgresqlDatabaseEnvName = "POSTGRESQL_DATABASE"
)

type PostgresqlConfig struct {
	Host     string
	Port     uint16
	User     string
	Password string
	Database string
}

func NewPostgresqlConfig() (*PostgresqlConfig, error) {
	envs := []string{
		postgresqlHostEnvName,
		postgresqlPortEnvName,
		postgresqlUserEnvName,
		postgresqlPasswordEnvName,
		postgresqlDatabaseEnvName,
	}

	values := make(map[string]string)

	for _, env := range envs {
		value := os.Getenv(env)

		if len(value) == 0 {
			return nil, errors.New(fmt.Sprintf("%s not found", env))
		}

		values[env] = value
	}

	port, err := strconv.ParseUint(values[postgresqlPortEnvName], 10, 16)
	if err != nil || port < 0 || port > 65535 {
		return nil, errors.New(fmt.Sprintf("%s is incorrect", postgresqlPortEnvName))
	}

	return &PostgresqlConfig{
		Host:     values[postgresqlHostEnvName],
		Port:     uint16(port),
		User:     values[postgresqlUserEnvName],
		Password: values[postgresqlPasswordEnvName],
		Database: values[postgresqlDatabaseEnvName],
	}, nil
}
