package usecases

type myService interface {
}

type UseCase struct {
	myService myService
}

func NewUseCase(service myService) *UseCase {
	return &UseCase{
		myService: service,
	}
}
