# **Computação Gráfica - Relatório**
## **Shader com colisões**




#### Trabalho realizdo por:

- António Rodrigues - a22202884

#### Relatório:

Comecei por procurar o “óbvio” e pesquisar como fazer um shader que reagia a colisões, com isso encontrei dois videos:

[Shader Graph Forcefield: Update](https://www.youtube.com/watch?v=P47yMdetoE4&ab_channel=WilmerLinGASchool) - Acabei por sentir que este vídeo não fazia o que queria, usava raycasts para determinar os impactos, e não colisões, mas retirei de lá a ideia de usar um ripple effect.

[Energy Shield Effect in Unity URP Shader Graph](https://www.youtube.com/watch?v=o4CGL2YXs5k&ab_channel=Imphenzia) - Este fazia quase tudo o que queria e usei como referência para começar.

Depois disto achei que procurar por um ripple effect seria o melhor começo, pois a partir daí, caso conseguisse determinar a posição das colisões, poderia enviar essa informação para o shader, e causar os ripple effects no local da colisão.

Pesquisei então por ripple effects e encontrei este video: 

[Shockwave Shader Graph - How to make a shock wave shader in Unity URP/HDRP](https://www.youtube.com/watch?v=dFDAwT5iozo&ab_channel=GameDevBill) 

Este video foi um bom começo, e permitiu-me chegar a um resultado parecido ao efeito que queria:

![Efeito ripple em sprite](https://media.discordapp.net/attachments/1163146681064357908/1191715615648530492/image.png?ex=65a672a7&is=6593fda7&hm=b81a781def3c2e51267f32dceef996ccaa3d4d1447a411394691035881e6ec89&=&format=webp&quality=lossless&width=746&height=407)
                                                    
Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1o_DCMi-iCpm2Wfh4DWscVADBldpeMY7n/view?usp=sharing)

Mas depois disto senti que tinha pouco controlo, no primeiro que era específico para sprites senti-me me com mais controlo pois o shader permitia mudar o focal point num espaço 2D facilmente, e consegui também fazer um efeito com mais que um ripple:

![Efeito ripple em sprite](https://media.discordapp.net/attachments/1163146681064357908/1191715682824491098/image.png?ex=65a672b7&is=6593fdb7&hm=a546eec052eab20fb6a1575d3975ea9a1e95d002b209614dc0fbb630bba6631b&=&format=webp&quality=lossless&width=696&height=272)

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1GrYGJ1sYELv5E832-VCtKjNdcqq5AHFU/view?usp=sharing)

Mas no caso da esfera, senti que tinha pouco controlo, e mesmo depois de algumas mudanças, e alterando o focal pointo para um Vector3, eu conseguia controlá-lo facilmente no eixo do X e do Y, mas estava com dificuldades no eixo do Z, garantidamente que era por não ter entendido tudo o que retirei do vídeo anterior, mas decidi voltar a pesquisar.

Com isso encontrei este vídeo:
[Unity Shader Graph VFX - Bubble Shield (Tutorial)](https://www.youtube.com/watch?v=jdAbVkre8cw&ab_channel=ABitOfGameDev)

Com este tutorial aprendi algus efeitos como o twirl, que achei que seriam interessantes para o shader final, mas este tutorial usava uma esfera com UVs alterados, e eu quero criar um shader que funcione com qualquer objeto, especialmente os default do Unity:

![Twirl](https://media.discordapp.net/attachments/1163146681064357908/1191715707428274286/image.png?ex=65a672bd&is=6593fdbd&hm=9607fa7bc45a8512d1ff2fc9c035e538c2bc5bd26930d70e9f256543a60ae93e&=&format=webp&quality=lossless&width=590&height=391)


Apesar de o material estar a mudar, a esfera em si não mudava, suspeitei que fosse  por a esfera do vídeo ter UVs alterados, não tinha maya como no vídeo mas instalei blender para confirmar a teoria, após criar a esfera com os UVs como no tutorial, a esfera continuou sem deformação:

![Sem deformação](https://media.discordapp.net/attachments/1163146681064357908/1191715738969448558/image.png?ex=65a672c5&is=6593fdc5&hm=49641b2a1b3b3a8482846a2861403279ea0a20d5d69e98554ef42e5b1e06710e&=&format=webp&quality=lossless&width=527&height=473)

Depois disto achei que já tinha material suficiente para começar o meu shader.


## **Comecei a fazer o meu shader:**

Criei um timer que me permite ir de 0 a 1 com a função sen:
![Timer Sin](https://media.discordapp.net/attachments/1163146681064357908/1191731228030795877/image.png?ex=65a68132&is=65940c32&hm=ad78c97a8120f0bcace8d953803991cc787aef3f74d375555e10e55592207bee&=&format=webp&quality=lossless&width=1348&height=550)

Em vez de usar diretamente o sine time, usamos o multiply pelo meio, para podemos alterar a frequência do time, e depois fazemos o sine, assim mantemos o intervalo entre 0 e 1 mas mudamos quanto oscila por segundo.
Criei isto com o intuito de começar por ter um bubbling effect, então a oscilação constante do sen era perfeita para isso.
Depois disso criei um grupo para mudar a posição dos vértices: para isso multiplico o resultado do grupo anterior pela normal dos vértices, depois adiciono essa mudança a posição de cada vértices, que vai ser na direção perpendicular ao vértices (o vetor normal de cada vértice), criando este efeito de expansão:

![Diagrama Vertices](https://media.discordapp.net/attachments/1163146681064357908/1191715793948389427/image.png?ex=65a672d2&is=6593fdd2&hm=063bbb9536e778aec98c037a7f9e78307828328dcfaf67f170d1aaa3558ff51c&=&format=webp&quality=lossless&width=498&height=363)

(Desenho que exemplifica a direção de cada vetor normal para cada vértice)

![Nodes Vertices](https://media.discordapp.net/attachments/1163146681064357908/1191730462641618954/image.png?ex=65a6807b&is=65940b7b&hm=874278795eafcbb532b5e85652bea378270c0fa65a9fb1fc5a4d082e72f8e5fd&=&format=webp&quality=lossless&width=668&height=670)

Com isto consegui o efeito pretendido:

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/153m3BaKkgHv-GbG429akcZVEX9hsjYSp/view?usp=sharing)

Usando agora alguns componentes que vi serem usados num dos vídeos, criei o anel que percorre a mesh:

![Nodes for ring](https://media.discordapp.net/attachments/1163146681064357908/1191719104504143912/image.png?ex=65a675e7&is=659400e7&hm=cbdd74181728155634a64ebe56961cb281fd19a572a6055e7d3af4076e0f8f8f&=&format=webp&quality=lossless&width=1440&height=540)

Recebemos o valor para a progressão da onda, ou seja onde está o anel, e usamos o componente fraction que nos dá a parte decimal do valor, depois pegamos neste valor e adicionamos-lhe o size, e noutro node subtraímos o size a ele, um exemplo disto: caso o valor estivesse no 0.6, e o size fosse 0.1, teríamos os valores 0.7 e 0.5, tendo assim uma wave de tamanho 0.2:

![Fraction node](https://media.discordapp.net/attachments/1163146681064357908/1191719249505419346/image.png?ex=65a6760a&is=6594010a&hm=fc932fe8d051d077c5ca6ac755a71c551c5b2024e7b3c446f079f1170c0d479e&=&format=webp&quality=lossless&width=837&height=643)



Depois disso, crio uma secção à parte, esta secção recebe o ponto de impacto (FocalPoint), normaliza este vetor para obter a direção, e neste caso, como estamos a aplicar uma esfera, dividimos por 2, pois queremos o comprimento de vetor a 0.5, já que o raio da esfera seria 0.5 e o diâmetro 1, as posições da mesh estão sempre compreendidas entre -0.5 e 0.5. Depois subtraímos a direção pela posição no objeto, para termos a posição inicial da shockwave no objeto. O node da length dá-nos a distância até esta posição inicial no objeto:

![Nodes for focal point](https://media.discordapp.net/attachments/1163146681064357908/1191719421551583293/image.png?ex=65a67633&is=65940133&hm=92cedcc69fa72013445ccdcda41f7ebbbfda8c5eee2b002fd4c3015b16e6b37c&=&format=webp&quality=lossless&width=1077&height=668)

Criei um node smoothstep para receber estes inputs, ao receber o add e o subtract, obtemos os limites do nosso anel e a length dá-nos o ponto inicial a partir de qual o anel se afasta e expande ao longo da mesh:

![Smoothstep Node](https://media.discordapp.net/attachments/1163146681064357908/1191725059522441318/image.png?ex=65a67b73&is=65940673&hm=7d76d4e329af7f3825b69a8676e4e24f8ffc776277d1e37f4129d172bce63e37&=&format=webp&quality=lossless&width=593&height=668)

Dou este resultado a um One Minus node, como o smoothstep vai estar sempre compreendido entre 0 e 1, o one minus acaba por nos dar a diferença do seu input. Dando o valor oposto ao smoothstep. Quando multiplicamos os dois resultados, tudo o que está a 0 em ambos é removido no outro, e onde não houver zeros, ficamos com um gradiente, que é mais forte no centro (onde ambos os inputs têm valor mais alto):

![One minus node](https://media.discordapp.net/attachments/1163146681064357908/1191726049508200528/image.png?ex=65a67c5f&is=6594075f&hm=526614aadb30b5debd7fc4452281b78f64a4b7bf5170a332b09b0662bedb5e47&=&format=webp&quality=lossless&width=895&height=670)


Multiplicamos este valor pela nossa amplitude, o que altera o tamanho dos nossos valores que não são zero, fazendo assim com que a onda aumente ou diminua de tamanho:

![Amplitude](https://media.discordapp.net/attachments/1163146681064357908/1191728549648924803/image.png?ex=65a67eb3&is=659409b3&hm=603ba354f507ed5fc0071dc92a2d093da28de71a62c8d0352abe7d0e841272fc&=&format=webp&quality=lossless&width=716&height=670)

Finalmente, damos este resultado ao nosso grupo que altera a posição dos vértices e obtemos a deformação:

![Final node connection](https://media.discordapp.net/attachments/1163146681064357908/1191729117985525871/image.png?ex=65a67f3a&is=65940a3a&hm=2db841514aed86cb5f361095a4b9a88102701dbbdf69f53e229d8e8cf2d4a7c5&=&format=webp&quality=lossless&width=888&height=670)

![Distortion Effect](https://media.discordapp.net/attachments/1163146681064357908/1191730238179262474/image.png?ex=65a68046&is=65940b46&hm=bb92cb868b2b1f10d0cf48b812e7fb3482f3533159d16dca43316f8e1d301a0a&=&format=webp&quality=lossless&width=426&height=368)

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1PPWpBOCNbuVdiQQhesbG8JtEANb8bVj7/view?usp=sharing)

Temos agora o efeito pretendido, mas não o queremos a repetir com o tempo como está agora, ou seja o valor da progressão da onda vai deixar de ser oscilante, mas sim um valor que o programa controla, e queremos que comece onde haja colisões, para isso temos que criar um script que trate de dar os valores corretos ao shader.

Antes disso, criei um subshader mais organizado para o efeito.
Este subshader recebe cinco variaveis:
- Progression: Distância do ponto de impacto entre 0 e 1, em que 0 é no ponto de impacto e 1 o ponto final;
- Focalpoint: O ponto inicial de impacto;
- Amplitude: A altura da onda;
- Size: O tamanho da onda;

O subshader devolve o anel para ser desenhado.

![SubShader](https://media.discordapp.net/attachments/1163146681064357908/1191816005484286033/image.png?ex=65a6d026&is=65945b26&hm=d80b4b31cfc8b9441a76b6e047f1d59048a6f3c02dba2e52b8bfa9fcc8144e97&=&format=webp&quality=lossless&width=1440&height=526)

Agora com o subshader feito, vamos criar o script que deteta as colisões e dá os dados da mesma ao shader:

No nosso script temos 4 variáveis:
- material: O material do nosso objeto, que neste caso tem que ter o shader criado anteriormente;
- defaultFocalPoint: Esta variável é criada como a posição nula, para quando a onda termina, voltarmos a posição inicial para evitar deformações;
- maxFocalPoints: Isto define quantas ondas permitimos ao mesmo tempo no nosso objeto, terá sempre um *hard limit* definido pelo shader;
- index: Quando temos mais que uma onda, o index permite percorrer cada onda para ser desenhada, caso cheguemos ao limite definido pelo maxFocalPoints, a primeira onda será substituida pela nova onda;
- destroyCollidedObjects: Um booleano que da a opção ao utilizar de destruir os objetos que colidem com o objeto ou não;
- frequency: Quão rapido a onda se propaga.

![Variables script](https://media.discordapp.net/attachments/1163146681064357908/1191816442946002994/image.png?ex=65a6d08e&is=65945b8e&hm=4331287f3d2dfb8c6447b8320c47b01212e7a5212adb24f6e37f322473ca78c0&=&format=webp&quality=lossless&width=615&height=293)

Assim que o programa corre, recebemos o material no objeto, definimos o defaultFocalPoint para dar reset as ondas, e damos este defaultFocalPoint para o focal point de cada onda para que comece tudo sem ondas. Inicializamos também o índice a 0.

![Start Method](https://media.discordapp.net/attachments/1163146681064357908/1191737523362336788/image.png?ex=65a6870e&is=6594120e&hm=6efd582b1e89373957e93fe0fb3808bac06d5d90b334405b879368f38e2488f7&=&format=webp&quality=lossless&width=748&height=403)

Durante o update, percorremos todos as ondas possíveis, crio uma variavel auxiliar para guardar a progressão da onda atual. 
Depois verifico se esta progressão está compreendida entre 0 e 1, se sim, incrementamos e multiplicamos a incrementação pela frequencia e pelo Time.deltaTime para que seja constante entre dispositivos, e não depender da frame rate. Depois atualizamos o valor da progressão da onda atual por este valor.
Caso a progressão seja maior ou igual a 1, voltamos a pôr a onda na posição inicial, e mudamos a progressão para 0 que é também o valor inicial da progressão

![Update Method](https://media.discordapp.net/attachments/1163146681064357908/1191816652078207089/image.png?ex=65a6d0c0&is=65945bc0&hm=285a5095623d0d9aa575ecddf0772b6315e37c9cb1717fb56585c603efd68fbc&=&format=webp&quality=lossless&width=788&height=650)

Quando detetamos uma colisão, recebemos todos os contact points desta colisão, e onde eles ocorrerem queremos iniciar uma onda. Para isso, usamos o InverseTransformPoint que nos dá a posição local, em relação ao transform do objeto em que o script está, do ponto de colisão. Damos este posição ao primeiro FocalPoint disponível, que no caso duma primeira colisão seria o 0.

Uso aqui também a expressão "index % maxFocalPoints" que me dará apenas o resto da divisão pelos focalPoints, garantido assim que apenas verificaremos o número de waves maximas definidas pelo script. Se por exemplo tivéssemos maxFocalPoints = 3, teríamos sempre os valores 0, 1 e 2.

Depois disto inicializo o valor da progressão a 0.001, para que o Update saiba que tem que começar a incrementar a progressão naquele nível.

Pensei também que poderia ser necessário, caso o programa corresse durante muito tempo, haver uma verificação pela valor máximo de int, caso houvesse 2147483647 colisões, mas para esta situação assumi que isto seria um caso extremamente raro, então deixei apenas comentado para que não fosse completamente descartado.

Decidi dar a opção ao utilizador se quer que os objetos colididos sejam destruídos ou não.

![Collision Detection](https://media.discordapp.net/attachments/1163146681064357908/1191740141325275207/image.png?ex=65a6897f&is=6594147f&hm=df144128c3a6c11dcd67483f1e43d028fd697930ad50ed0d0bcd729109580861&=&format=webp&quality=lossless&width=1281&height=502)

Depois disto, para que o shader permitisse mais que uma onda, tive que criar mais variaveis:

![Variables Shader](https://media.discordapp.net/attachments/1163146681064357908/1191805070132658367/image.png?ex=65a6c5f7&is=659450f7&hm=a8ede5e63669c2e60111ca7483fb29821154ebc64056f74d7f9e43d26ba72564&=&format=webp&quality=lossless&width=276&height=670)

Decidi que o shader permitira um máximo de 7 ondas simultâneas:

![7 Waves](https://media.discordapp.net/attachments/267705945377734667/1191822323460870234/image.png?ex=65a6d608&is=65946108&hm=893ac8d967d5f6cf53d388ccd50b95e1345009dbaafc03f1c203dd2d90000ccc&=&format=webp&quality=lossless&width=1213&height=668)

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/18KxEmDjmmkRf2Alndq7GpWrjbjcEhu6O/view?usp=sharing)

Demonstração do efeito com rotação: [Vídeo](https://drive.google.com/file/d/1Kz_kpQA-vv305fSEpCzc_p0d8Rl1MWBe/view?usp=sharing)

Em seguida decidi que já estava farto dos tijolos, então comecei a trabalhar para fazer um efeito para o escudo, primeiro deparei-me com este tutorial:

[Unity Shader Graph - Sci-Fi Barrier / Shield Tutorial](https://www.youtube.com/watch?v=rB4YMQmO8Mw&t=3s&ab_channel=GabrielAguiarProd.) - Repliquei o tutorial e percebi melhor como funcionava o *noise* e os padrões procedimentais. 

Depois de muitos testes e exprimentações criei o seguinte shader:

![Cell Shader](https://media.discordapp.net/attachments/267705945377734667/1191853749472350268/image.png?ex=65a6f34d&is=65947e4d&hm=020ae28a6005194543573027b3eefe78a37411626dbeadee6c2ae117a0d80a87&=&format=webp&quality=lossless&width=1193&height=676)

