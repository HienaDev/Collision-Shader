Comecei por procurar o “óbvio” e pesquisar como fazer um shader que reagia a colisões, com isso encontrei dois videos:
[Shader Graph Forcefield: Update](https://www.youtube.com/watch?v=P47yMdetoE4&ab_channel=WilmerLinGASchool) - Acabei por sentir que este vídeo não fazia o que queria, usava raycasts para determinar os impactos, e não colisões, mas retirei de lá a ideia de usar um ripple effect.
[Energy Shield Effect in Unity URP Shader Graph] (https://www.youtube.com/watch?v=o4CGL2YXs5k&ab_channel=Imphenzia) - Este fazia quase tudo o que queria e usei como referência para começar.

Depois disto achei que procurar por um ripple effect seria o melhor começo, pois a partir daí, caso conseguisse determinar a posição das colisões, poderia enviar essa informação para o shader, e causar os ripple effects no local da colisão.

Pesquisei então por ripple effects e encontrei este video: 
[Shockwave Shader Graph - How to make a shock wave shader in Unity URP/HDRP](https://www.youtube.com/watch?v=dFDAwT5iozo&ab_channel=GameDevBill) 

Este video foi um bom começo, e permitiu-me chegar a um resultado parecido ao efeito que queria:
![Efeito ripple em sprite](https://media.discordapp.net/attachments/1163146681064357908/1191715615648530492/image.png?ex=65a672a7&is=6593fda7&hm=b81a781def3c2e51267f32dceef996ccaa3d4d1447a411394691035881e6ec89&=&format=webp&quality=lossless&width=746&height=407)
                                                    [Vídeo](https://drive.google.com/file/d/1o_DCMi-iCpm2Wfh4DWscVADBldpeMY7n/view?usp=sharing)

Mas depois disto senti que tinha pouco controlo, no primeiro que era específico para sprites senti-me me com mais controlo pois o shader permitia mudar o focal point num espaço 2D facilmente, e consegui também fazer um efeito com mais que um ripple:

Vídeo

Mas no caso da esfera, senti que tinha pouco controlo, e mesmo depois de algumas mudanças, e alterando o focal pointo para um Vector3, eu conseguia controlá-lo facilmente no eixo do X e do Y, mas estava com dificuldades no eixo do Z, garantidamente que era por não ter entendido tudo o que retirei do vídeo anterior, mas decidi voltar a pesquisar:

Com isso encontrei este vídeo:
https://www.youtube.com/watch?v=jdAbVkre8cw&ab_channel=ABitOfGameDev 

Com este tutorial aprendi algus efeitos como o twirl, que achei que seriam interessantes para o shader final, mas este tutorial usava uma esfera com UVs alterados, e eu quero criar um shader que funcione com qualquer objeto, especialmente os default do Unity:


Apesar de o material estar a mudar, a esfera em si não mudava, suspeitei que fosse  por a esfera do vídeo ter UVs alterados, não tinha maya como no vídeo mas instalei blender para confirmar a teoria, após criar a esfera com os UVs como no tutorial, a esfera continuou sem deformação:


Depois disto achei que já tinha material suficiente para começar o meu shader:


Comecei a fazer o meu shader:

Criei um timer que me permite ir de 0 a 1 com a função sen:

Em vez de usar diretamente o sine time, usamos o multiply pelo meio, para podemos alterar a frequência do time, e depois fazemos o sine, assim mantemos o intervalo entre 0 e 1 mas mudamos quanto oscila por segundo.
Criei isto com o intuito de começar por ter um bubbling effect, então a oscilação constante do sen era perfeita para isso.
Depois disso criei um grupo para mudar a posição dos vértices: para isso multiplico o resultado do grupo anterior pela normal dos vértices, depois adiciono essa mudança a posição de cada vértices, que vai ser na direção perpendicular ao vértices (o vetor normal de cada vértice), criando este efeito de expansão:

(Diagrama que exemplifica a direção de cada vetor normal para cada vértice)

Com isto consegui o efeito pretendido:
https://drive.google.com/file/d/153m3BaKkgHv-GbG429akcZVEX9hsjYSp/view?usp=sharing 

Usando agora alguns componentes que vi serem usados num dos vídeos, criei o anel que percorre a mesh:


Recebemos o valor para a progressão da onda, ou seja onde está o anel, e usamos o componente fraction que nos dá a parte decimal do valor, depois pegamos neste valor e adicionamos-lhe o size, e noutro node subtraímos o size a ele, um exemplo disto: caso o valor estivesse no 0.6, e o size fosse 0.1, teríamos os valores 0.7 e 0.5, tendo assim uma wave de tamanho 0.2:





Depois disso, crio uma secção à parte, esta secção vai me dar




