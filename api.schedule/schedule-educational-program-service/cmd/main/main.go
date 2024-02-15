package main

import "log"

func main() {
	provider := newServiceProvider()
	if err := provider.migrator.Migrate(); err != nil {
		log.Println(err)
	}

	func() {
		err := provider.httpServer.Run()
		if err != nil {
			panic(err)
		}
	}()
}
