import java.io.FileInputStream;
import java.io.IOException;


public class JacHash {
	public int MAX_LENGTH = 16;
	public byte FILLER_BYTE = 0xF;
	
	private int a;
	private int b;
	private int c;
	private int d;
	private int x;
	
	public JacHash() {
	}
	
	public String computeHashFromString(String text) {
		return computeHashFromBytes(text.getBytes());
	}
	
	public String computeHashFromBytes(byte[] bytes) {
		init();
		byte[] source = pad(bytes);
		byte[] result = new byte[MAX_LENGTH];
		for (int i = 0; i < source.length; i++)
			x += source[i];
		for (int i = 0; i < source.length; i++)
			result[i % MAX_LENGTH] = transformByte(source[i]);
		String accum = "";
		for (int i = 0; i < MAX_LENGTH; i++)
			accum += String.format("%02x", result[i]);
		return accum;
	}
	
	public String computeHashFromFile(String filePath) throws IOException {
		init();
		FileInputStream reader = new FileInputStream(filePath);
		long length = reader.getChannel().size();
		int appendToStream = 0;
		if (length < MAX_LENGTH)
			appendToStream = MAX_LENGTH;
		while (reader.getChannel().position() < length)
			x += reader.read();
		reader.getChannel().position(0);
		for (int i = 0; i < appendToStream; i++)
			x += FILLER_BYTE;
		byte[] result = new byte[MAX_LENGTH];
		while (reader.getChannel().position() < length)
			result[(int) (reader.getChannel().position() % MAX_LENGTH)] = transformByte((byte)reader.read());
		String accum = "";
		for (int i = 0; i < MAX_LENGTH; i++)
			accum += String.format("%02x", result[i]);
		reader.close();
		return accum;
	}
	
	private byte transformByte(byte bl) {
		a = rotateLeft(bl, x);
		b = (b ^ bl) - x;
		c = (a + b) & x;
		d ^= x - b;
		x ^= d;
		
		return (byte) ((a * c) + b - x * d ^ bl);
	}
	
	private byte rotateLeft(byte b, int bits) {
		return (byte) ((b << bits) | b >> 32 - bits);
	}
	
	private void init() {
		a = 0x6B87;
		b = 0x7F43;
		c = 0xA4Ad;
		d = 0xDC3F;
		x = 0;
	}
	
	private byte[] pad(byte[] bytes) {
		if (bytes.length >= MAX_LENGTH)
			return bytes;
		byte[] result = new byte[MAX_LENGTH];
		for (int i = 0; i < bytes.length; i++)
			result[i] = bytes[i];
		for (int i = bytes.length; i < result.length; i++) {
			result[i] = FILLER_BYTE;
		}
		return result;
	}
}
