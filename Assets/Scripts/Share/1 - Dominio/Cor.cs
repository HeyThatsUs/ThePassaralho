public class Cor
{
    private byte _vermelho;
    private byte _verde;
    private byte _azul;
    private byte _transparencia;

    public byte Vermelho
    {
        get { return _vermelho; }
    }
    public byte Verde
    {
        get { return _verde; }
    }
    public byte Azul
    {
        get { return _azul; }
    }
    public byte Transparencia
    {
        get { return _transparencia; }
    }

    public Cor()
    {
        _vermelho = 0;
        _verde = 0;
        _azul = 0;
        _transparencia = 255;
    }

    public void DefinirCor(byte vermelho, byte verde, byte azul, byte transparencia = 255)
    {
        _vermelho = vermelho;
        _verde = verde;
        _azul = azul;
        _transparencia = transparencia;
    }
}
