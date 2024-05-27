# Chaos Engineering Laboratory

Bem-vindo ao projeto de Laboratório de Chaos Engineering em C#! Este repositório contém conceitos de Chaos Engineering na comunicação com serviços externos usando C#. O objetivo é demonstrar como introduzir e mitigar falhas de maneira controlada para melhorar a resiliência do sistema.

## Introdução
Chaos Engineering é a prática de experimentar em um sistema ao induzir falhas controladas para construir confiança na capacidade do sistema de suportar condições adversas. Este projeto foca em aplicar Chaos Engineering em interações com serviços externos usando C#.

## Pré-requisitos
Antes de começar, certifique-se de ter o seguinte software instalado:

.NET 6 SDK
Git
Visual Studio Code ou Visual Studio

## Instalação

1 - Clone o repositório::

```sh
git clone https://github.com/seu-usuario/ChaosEngineeringLab.git
```

2 - Navegue até o diretório do projeto:

```sh
cd Lab.ChaosEngineering
```

3 - Restaure as dependências do projeto:

```sh
dotnet restore
```

4 - Construir e iniciar os serviços:

```sh
dotnet run --project src/Lab.ChaosEngineering
```

## Introduzindo Falhas
Os exemplos de Chaos Engineering estão localizados no diretório Chaos dentro do projeto principal. Eles demonstram como introduzir diferentes tipos de falhas na comunicação com serviços externos, como:

Latência artificial
Falhas de rede
Exceções simuladas

