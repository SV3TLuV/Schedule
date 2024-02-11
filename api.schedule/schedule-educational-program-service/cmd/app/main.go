package main

func main() {
	provider := newServiceProvider()
	provider.migrator.Migrate()
}
