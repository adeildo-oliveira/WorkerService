[![Build status](https://ci.appveyor.com/api/projects/status/urbut5r45bv7c9cq?svg=true)](https://ci.appveyor.com/project/adeildo-oliveira/workerservice)
# Criando um serviço no Ubuntu (Linux)
No Ubuntu (Linux) precisamos instalar o runtime do **Asp.Net Core** para que o nosso serviço consiga executar.

```
sudo apt-get update
sudo apt-get install apt-transport-https
sudo apt-get update
sudo apt-get install dotnet-runtime-3.1
```
Para mais detalhes, segue a [documentação](https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-ubuntu-1904) da instalação.

Com o runtime instaldo, precisamos criar o template do serviço para executar no Linux.

Exemplo:
```
[Unit]
Description=descrição do serviço

[Service]
Type=notify
User=root
ExecStart=/home/Documentos/MeuServico
Restart=on-abort

[Install]
WantedBy=multi-user.target
```
```ExecStart```: diretório aonde estão as dlls, configs e etc.
Os detalhes podem ser consultados [aqui](https://devblogs.microsoft.com/dotnet/net-core-and-systemd/).

O template deve ser salvo no diretório **/etc/systemd/system/** com a extensão `.service`.

Exemplo: /etc/systemd/system/**NomeDoMeuServico`.service`**

Caso não seja possível salvar o arquivo no diretório **/etc/systemd/system/**, você pode usar algum editor de texto Notepad++, VS Code, Sublime... no modo administrador. Abaixo, segue um exemplo de como acessar o [VS Code no modo administrador](https://askubuntu.com/questions/803343/how-to-run-visual-studio-code-as-root).

    sudo code --user-data-dir="~/.vscode-root"

Feito isso, precisamos atualizar o repositório de serviço.
```
sudo systemctl daemon-reload
```

Precisamos também, habilitar o serviço.
```
sudo systemctl enable testapp.service
```

Para mostrar o status do serviço, podemos usar o seguinte comando.
```
sudo systemctl status testapp
```

Por fim, iniciar o serviço.
```
sudo systemctl start testapp.service
```

E para visualizar o log de execução.
```
sudo journalctl -u testapp
```

No seu projeto WorkerService, você precisa informar o tipo do sistema operacional, no nosso caso, Linux.
1. Adicionar o package ``Install-Package Microsoft.Extensions.Hosting.WindowsServices``. A classe program do seu projeto deve ficar conforme exemplo:

```
public static IHostBuilder CreateHostBuilder(string[] args) => 
        Host.CreateDefaultBuilder(args)
        .UseWindowsService() //Windows Service
        .UseSystemd()       //Linux Service
        .ConfigureServices((hostContext, services) =>
        {
            services.AddHostedService<Worker>();
        });
```
Ao fazer o publish do seu serviço, será gerado as dlls. No template para rodar/criar o serviço do Linux. Informamos o nome do serviço que será gerado **SEM** extensão.

```
dotnet publish --self-contained -r linux-x64 -o MeuDiretorio\worke_service
```

Exemplo:
* ~~MeuServico.dll~~
* MeuServico

# Criando um serviço no Windows
1. Criando o serviço:
   ```
   sc create MeuServico DisplayName="Meu Serviço" binPath="Diretorio\MeuServico.exe"
   ```
2. Iniciando o Serviço
    ```
    sc start DemoService
    ```
3. Parando o serviço
    ```
    sc stop DemoService
    ```
4. Excluindo o serviço
    ```
    sc delete DemoService
    ```
Para mais detalhes, acesse [aqui](https://codeburst.io/create-a-windows-service-app-in-net-core-3-0-5ecb29fb5ad0
).

```
dotnet publish --self-contained -r win10-x64 -o H:\publish\worke_service
```