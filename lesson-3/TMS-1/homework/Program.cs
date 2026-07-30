using TMS_1;

Player player = new Player("Player");
Player computer = new Player("Computer");

Game game = new Game(player, computer, 5);

game.Play();