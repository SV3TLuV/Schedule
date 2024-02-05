package config

import (
	"errors"
	"fmt"
	"os"
)

const (
	apiHostEnvName = "API_HOST"
	apiPortEnvName = "API_PORT"
)

type ApiConfiguration struct {
	Host string
	Port string
}

func NewApiConfiguration() (*ApiConfiguration, error) {
	envs := []string{
		apiHostEnvName,
		apiPortEnvName,
	}

	values := make(map[string]string)

	for _, env := range envs {
		value := os.Getenv(env)

		if len(value) == 0 {
			return nil, errors.New(fmt.Sprintf("%s not found", env))
		}

		values[env] = value
	}

	return &ApiConfiguration{
		Host: values[apiHostEnvName],
		Port: values[apiPortEnvName],
	}, nil
}

func (cfg *ApiConfiguration) Address() string {
	return fmt.Sprintf("%s:%s", cfg.Host, cfg.Port)
}
