using Assets.Models;
public class ArquivoSaveControlador: ArquivosControlador<SaveFile>
{
    public ArquivoSaveControlador() : base(new SaveFile())
    {

    }
}
