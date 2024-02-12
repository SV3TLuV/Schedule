package main

func main() {
	provider := newServiceProvider()
	provider.migrator.Migrate()

	func() {
		err := provider.httpServer.Run()
		if err != nil {
			panic(err)
		}
	}()
}
