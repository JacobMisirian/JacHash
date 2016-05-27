import java.io.IOException;


public class Main {
	public static void main(String[] args) throws IOException {
		if (args.length < 2) {
			System.out.println("Not enough arguments!");
			return;
		}
		if (args[0].equals("-s"))
			System.out.println(new JacHash().computeHashFromString(args[1]));
		else if (args[0].equals("-f"))
			System.out.println(new JacHash().computeHashFromFile(args[1]));
	}
}
