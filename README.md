# **Computação Gráfica - Relatório**
## ***Shader* com colisões**


![Final *Shader* Images](https://media.discordapp.net/attachments/1163146681064357908/1192491092122292325/image.png?ex=65a944df&is=6596cfdf&hm=65a6e68bf7295b35c9ff6d92a5ae85a8d1a48d6ce678b4c4d02c4f634dd84b70&=&format=webp&quality=lossless&width=1440&height=293)

![Tree Shade Image](https://media.discordapp.net/attachments/1163146681064357908/1192533398397399170/image.png?ex=65a96c46&is=6596f746&hm=7aa932a65b2f6f469f6715d11db6508dc2ced36a46e723c375768dab4769a7a6&=&format=webp&quality=lossless&width=926&height=670)

![Statue *Shader* Image](https://media.discordapp.net/attachments/1163146681064357908/1192533465514659860/image.png?ex=65a96c56&is=6596f756&hm=c1d430a52cb90e5e1debdd698e18545244981b8fa28d39f2533aeee5b013e5a4&=&format=webp&quality=lossless&width=403&height=670)

## Trabalho realizdo por:

- António Rodrigues - a22202884.

## Relatório:

Comecei por procurar o “óbvio” e pesquisar como fazer um *shader* que reagia a colisões, com isso encontrei dois videos:

[*Shader* Graph Forcefield: Update](https://www.youtube.com/watch?v=P47yMdetoE4&ab_channel=WilmerLinGASchool) - Acabei por sentir que este vídeo não fazia o que queria, usava *raycasts* para determinar os impactos, e não colisões, mas retirei de lá a ideia de usar um *ripple effect*.

[Energy Shield Effect in Unity URP *Shader* Graph](https://www.youtube.com/watch?v=o4CGL2YXs5k&ab_channel=Imphenzia) - Este fazia quase tudo o que queria e usei como referência para começar.

Depois disto achei que procurar por um *ripple effect* seria o melhor começo, pois a partir daí, caso conseguisse determinar a posição das colisões, poderia enviar essa informação para o *shader*, e causar os *ripple effects* no local da colisão.

Pesquisei então por *ripple effects* e encontrei este video: 

[Shockwave *Shader* Graph - How to make a shock wave *shader* in Unity URP/HDRP](https://www.youtube.com/watch?v=dFDAwT5iozo&ab_channel=GameDevBill) 

Este video foi um bom começo, e permitiu-me chegar a um resultado parecido ao efeito que queria:

![Efeito *ripple* em *sprite*](https://media.discordapp.net/attachments/1163146681064357908/1191715615648530492/image.png?ex=65a672a7&is=6593fda7&hm=b81a781def3c2e51267f32dceef996ccaa3d4d1447a411394691035881e6ec89&=&format=webp&quality=lossless&width=746&height=407)
                                                    
Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1o_DCMi-iCpm2Wfh4DWscVADBldpeMY7n/view?usp=sharing)

Mas depois disto senti que tinha pouco controlo, no primeiro que era específico para *sprites* senti-me com mais controlo pois o *shader* permitia mudar o *focal point* num espaço 2D facilmente, e consegui também fazer um efeito com mais que um *ripple*:

![Efeito *ripple* em *sprite*](https://media.discordapp.net/attachments/1163146681064357908/1191715682824491098/image.png?ex=65a672b7&is=6593fdb7&hm=a546eec052eab20fb6a1575d3975ea9a1e95d002b209614dc0fbb630bba6631b&=&format=webp&quality=lossless&width=696&height=272)

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1GrYGJ1sYELv5E832-VCtKjNdcqq5AHFU/view?usp=sharing)

Mas no caso da esfera, senti que tinha pouco controlo, e mesmo depois de algumas mudanças, e alterando o *focal point* para um Vector3, eu conseguia controlá-lo facilmente no eixo do X e do Y, mas estava com dificuldades no eixo do Z, garantidamente que era por não ter entendido tudo o que retirei do vídeo anterior, mas decidi voltar a pesquisar.

Com isso encontrei este vídeo:
[Unity *Shader* Graph VFX - Bubble Shield (Tutorial)](https://www.youtube.com/watch?v=jdAbVkre8cw&ab_channel=ABitOfGameDev)

Com este tutorial aprendi algus efeitos como o *twirl*, que achei que seriam interessantes para o *shader* final, mas este tutorial usava uma esfera com UVs alterados, e eu quero criar um *shader* que funcione com qualquer objeto, especialmente os *default* do Unity:

![Twirl](https://media.discordapp.net/attachments/1163146681064357908/1191715707428274286/image.png?ex=65a672bd&is=6593fdbd&hm=9607fa7bc45a8512d1ff2fc9c035e538c2bc5bd26930d70e9f256543a60ae93e&=&format=webp&quality=lossless&width=590&height=391)

Apesar de o material estar a mudar, a esfera em si não mudava, suspeitei que fosse  por a esfera do vídeo ter UVs alterados, não tinha Maya como no vídeo mas instalei Blender para confirmar a teoria, após criar a esfera com os UVs como no tutorial, a esfera continuou sem deformação, acabei por não entender o porquê:

![Sem deformação](https://media.discordapp.net/attachments/1163146681064357908/1191715738969448558/image.png?ex=65a672c5&is=6593fdc5&hm=49641b2a1b3b3a8482846a2861403279ea0a20d5d69e98554ef42e5b1e06710e&=&format=webp&quality=lossless&width=527&height=473)

Mas depois disto achei que já tinha material suficiente para começar o meu *shader*.


## **Comecei a fazer o meu *shader*:**

Criei um temporizador que me permite ir de 0 a 1 com a função seno:
![Timer Sin](https://media.discordapp.net/attachments/1163146681064357908/1191731228030795877/image.png?ex=65a68132&is=65940c32&hm=ad78c97a8120f0bcace8d953803991cc787aef3f74d375555e10e55592207bee&=&format=webp&quality=lossless&width=1348&height=550)

Em vez de usar diretamente o *sine time*, usamos o *multiply* pelo meio, para podemos alterar a frequência do *time*, e depois fazemos o *sine*, assim mantemos o intervalo entre 0 e 1 mas mudamos quanto oscila por segundo.

Criei isto com o intuito de começar por ter um *bubbling effect*, então a oscilação constante do sen era perfeita para isso.

Depois disso criei um grupo para mudar a posição dos vértices: para isso multiplico o resultado do grupo anterior pela normal dos vértices, depois adiciono essa mudança a posição de cada vértices, que vai ser na direção perpendicular ao vértices (o vetor normal de cada vértice), criando este efeito de expansão:

![Diagrama Vertices](https://media.discordapp.net/attachments/1163146681064357908/1191715793948389427/image.png?ex=65a672d2&is=6593fdd2&hm=063bbb9536e778aec98c037a7f9e78307828328dcfaf67f170d1aaa3558ff51c&=&format=webp&quality=lossless&width=498&height=363)

(Desenho que exemplifica a direção de cada vetor normal para cada vértice)

![Nodes Vertices](https://media.discordapp.net/attachments/1163146681064357908/1191730462641618954/image.png?ex=65a6807b&is=65940b7b&hm=874278795eafcbb532b5e85652bea378270c0fa65a9fb1fc5a4d082e72f8e5fd&=&format=webp&quality=lossless&width=668&height=670)

Com isto consegui o efeito pretendido:

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/153m3BaKkgHv-GbG429akcZVEX9hsjYSp/view?usp=sharing)

Usando agora alguns componentes que vi serem usados num dos vídeos, criei o anel que percorre a *mesh*:

![Nodes for ring](https://media.discordapp.net/attachments/1163146681064357908/1191719104504143912/image.png?ex=65a675e7&is=659400e7&hm=cbdd74181728155634a64ebe56961cb281fd19a572a6055e7d3af4076e0f8f8f&=&format=webp&quality=lossless&width=1440&height=540)

Recebemos o valor para a progressão da onda, ou seja onde está o anel, e usamos o componente *fraction* que nos dá a parte decimal do valor, depois pegamos neste valor e adicionamos-lhe o size, e noutro node subtraímos o size a ele, um exemplo disto: caso o valor estivesse no 0.6, e o size fosse 0.1, teríamos os valores 0.7 e 0.5, tendo assim uma wave de tamanho 0.2:

![Fraction node](https://media.discordapp.net/attachments/1163146681064357908/1191719249505419346/image.png?ex=65a6760a&is=6594010a&hm=fc932fe8d051d077c5ca6ac755a71c551c5b2024e7b3c446f079f1170c0d479e&=&format=webp&quality=lossless&width=837&height=643)



Depois disso, crio uma secção à parte, esta secção recebe o ponto de impacto (FocalPoint), normaliza este vetor para obter a direção. 

Neste caso, como estamos a aplicar uma esfera de tamanho 1, dividimos por 2, pois queremos o comprimento de vetor a 0.5, já que o raio da esfera seria 0.5 e o diâmetro 1, as esferas neste caso teriam de ter sempre tamanho 1. 

Depois subtraímos a direção pela posição no objeto, para termos a posição inicial da shockwave no objeto. O node da length dá-nos a distância até esta posição inicial no objeto:

![Nodes for *focal point*](https://media.discordapp.net/attachments/1163146681064357908/1191719421551583293/image.png?ex=65a67633&is=65940133&hm=92cedcc69fa72013445ccdcda41f7ebbbfda8c5eee2b002fd4c3015b16e6b37c&=&format=webp&quality=lossless&width=1077&height=668)

Criei um node smoothstep para receber estes inputs, ao receber o add e o subtract, obtemos os limites do nosso anel e a length dá-nos o ponto inicial a partir de qual o anel se afasta e expande ao longo da *mesh*:

![Smoothstep Node](https://media.discordapp.net/attachments/1163146681064357908/1191725059522441318/image.png?ex=65a67b73&is=65940673&hm=7d76d4e329af7f3825b69a8676e4e24f8ffc776277d1e37f4129d172bce63e37&=&format=webp&quality=lossless&width=593&height=668)

Dou este resultado a um One Minus node, como o smoothstep vai estar sempre compreendido entre 0 e 1, o one minus acaba por nos dar a diferença do seu input. Dando o valor oposto ao smoothstep. 

Quando multiplicamos os dois resultados, tudo o que está a 0 em ambos é removido no outro, e onde não houver zeros, ficamos com um gradiente, que é mais forte no centro (onde ambos os inputs têm valor mais alto):

![One minus node](https://media.discordapp.net/attachments/1163146681064357908/1191726049508200528/image.png?ex=65a67c5f&is=6594075f&hm=526614aadb30b5debd7fc4452281b78f64a4b7bf5170a332b09b0662bedb5e47&=&format=webp&quality=lossless&width=895&height=670)


Multiplicamos este valor pela nossa amplitude, o que altera o tamanho dos nossos valores que não são zero, fazendo assim com que a onda aumente ou diminua de tamanho:

![Amplitude](https://media.discordapp.net/attachments/1163146681064357908/1191728549648924803/image.png?ex=65a67eb3&is=659409b3&hm=603ba354f507ed5fc0071dc92a2d093da28de71a62c8d0352abe7d0e841272fc&=&format=webp&quality=lossless&width=716&height=670)

Finalmente, damos este resultado ao nosso grupo que altera a posição dos vértices e obtemos a deformação:

![Final node connection](https://media.discordapp.net/attachments/1163146681064357908/1191729117985525871/image.png?ex=65a67f3a&is=65940a3a&hm=2db841514aed86cb5f361095a4b9a88102701dbbdf69f53e229d8e8cf2d4a7c5&=&format=webp&quality=lossless&width=888&height=670)

![Distortion Effect](https://media.discordapp.net/attachments/1163146681064357908/1191730238179262474/image.png?ex=65a68046&is=65940b46&hm=bb92cb868b2b1f10d0cf48b812e7fb3482f3533159d16dca43316f8e1d301a0a&=&format=webp&quality=lossless&width=426&height=368)

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1PPWpBOCNbuVdiQQhesbG8JtEANb8bVj7/view?usp=sharing)

Temos agora o efeito pretendido, mas não o queremos a repetir com o tempo como está agora, ou seja o valor da progressão da onda vai deixar de ser oscilante, mas sim um valor que o programa controla, e queremos que comece onde haja colisões, para isso temos que criar um *script* que trate de dar os valores corretos ao *shader*.

Antes disso, criei um sub*shader* mais organizado para o efeito.
Este sub*shader* recebe 4 variaveis:
- Progression: Distância do ponto de impacto entre 0 e 1, em que 0 é no ponto de impacto e 1 o ponto final;
- Focalpoint: O ponto inicial de impacto;
- Amplitude: A altura da onda;
- Size: O tamanho da onda;

O sub*shader* devolve o anel para ser desenhado.

![SubShader](https://media.discordapp.net/attachments/1163146681064357908/1191816005484286033/image.png?ex=65a6d026&is=65945b26&hm=d80b4b31cfc8b9441a76b6e047f1d59048a6f3c02dba2e52b8bfa9fcc8144e97&=&format=webp&quality=lossless&width=1440&height=526)

Agora com o sub*shader* feito, vamos criar o *script* que deteta as colisões e dá os dados da mesma ao *shader*:

No nosso *script* temos 6 variáveis:
- material: O material do nosso objeto, que neste caso tem que ter o *shader* criado anteriormente;
- defaultFocalPoint: Esta variável é criada como a posição nula, para quando a onda termina, voltarmos a posição inicial para evitar deformações;
- maxFocalPoints: Isto define quantas ondas permitimos ao mesmo tempo no nosso objeto, terá sempre um *hard limit* definido pelo *shader*;
- index: Quando temos mais que uma onda, o index permite percorrer cada onda para ser desenhada, caso cheguemos ao limite definido pelo maxFocalPoints, a primeira onda será substituida pela nova onda;
- destroyCollidedObjects: Um booleano que da a opção ao utilizar de destruir os objetos que colidem com o objeto ou não;
- frequency: Quão rapido a onda se propaga.

![Variables *script*](https://media.discordapp.net/attachments/1163146681064357908/1191816442946002994/image.png?ex=65a6d08e&is=65945b8e&hm=4331287f3d2dfb8c6447b8320c47b01212e7a5212adb24f6e37f322473ca78c0&=&format=webp&quality=lossless&width=615&height=293)

**Método Start**

Assim que o programa corre, recebemos o material no objeto, definimos o defaultFocalPoint para dar reset as ondas, e damos este defaultFocalPoint para o *focal point* de cada onda para que comece tudo sem ondas. Inicializamos também o índice a 0.

![Start Method](https://media.discordapp.net/attachments/1163146681064357908/1191737523362336788/image.png?ex=65a6870e&is=6594120e&hm=6efd582b1e89373957e93fe0fb3808bac06d5d90b334405b879368f38e2488f7&=&format=webp&quality=lossless&width=748&height=403)

**Método Update**

Durante o update, percorremos todos as ondas possíveis, crio uma variavel auxiliar para guardar a progressão da onda atual. 

Depois verifico se esta progressão está compreendida entre 0 e 1, se sim, incrementamos e multiplicamos a incrementação pela frequencia e pelo Time.deltaTime para que seja constante entre dispositivos, e não depender da frame rate. 

Depois atualizamos o valor da progressão da onda atual por este valor.

Caso a progressão seja maior ou igual a 1, voltamos a pôr a onda na posição inicial, e mudamos a progressão para 0 que é também o valor inicial da progressão

![Update Method](https://media.discordapp.net/attachments/1163146681064357908/1191816652078207089/image.png?ex=65a6d0c0&is=65945bc0&hm=285a5095623d0d9aa575ecddf0772b6315e37c9cb1717fb56585c603efd68fbc&=&format=webp&quality=lossless&width=788&height=650)

**Deteção de colisões:**

Quando detetamos uma colisão, recebemos todos os contact points desta colisão, e onde eles ocorrerem queremos iniciar uma onda.

Para isso, usamos o InverseTransformPoint que nos dá a posição local, em relação ao transform do objeto em que o *script* está, do ponto de colisão. Damos este posição ao primeiro FocalPoint disponível, que no caso duma primeira colisão seria o 0.

Uso aqui também a expressão "index % maxFocalPoints" que me dará apenas o resto da divisão pelos focalPoints, garantido assim que apenas verificaremos o número de waves maximas definidas pelo *script*. Se por exemplo tivéssemos maxFocalPoints = 3, teríamos sempre os valores 0, 1 e 2.

Depois disto inicializo o valor da progressão a 0.001, para que o Update saiba que tem que começar a incrementar a progressão naquele nível.

Pensei também que poderia ser necessário, caso o programa corresse durante muito tempo, haver uma verificação pela valor máximo de int, caso houvesse 2147483647 colisões, mas para esta situação assumi que isto seria um caso extremamente raro, então deixei apenas comentado para que não fosse completamente descartado.

Decidi dar a opção ao utilizador se quer que os objetos colididos sejam destruídos ou não.

![Collision Detection](https://media.discordapp.net/attachments/1163146681064357908/1191740141325275207/image.png?ex=65a6897f&is=6594147f&hm=df144128c3a6c11dcd67483f1e43d028fd697930ad50ed0d0bcd729109580861&=&format=webp&quality=lossless&width=1281&height=502)

Depois disto, para que o *shader* permitisse mais que uma onda, tive que criar mais variaveis:

![Variables *Shader*](https://media.discordapp.net/attachments/1163146681064357908/1191805070132658367/image.png?ex=65a6c5f7&is=659450f7&hm=a8ede5e63669c2e60111ca7483fb29821154ebc64056f74d7f9e43d26ba72564&=&format=webp&quality=lossless&width=276&height=670)

Decidi que o *shader* permitira um máximo de 7 ondas simultâneas:

![7 Waves](https://media.discordapp.net/attachments/267705945377734667/1191822323460870234/image.png?ex=65a6d608&is=65946108&hm=893ac8d967d5f6cf53d388ccd50b95e1345009dbaafc03f1c203dd2d90000ccc&=&format=webp&quality=lossless&width=1213&height=668)

Criei também uma nova esfera com mais triangulos com probuilder, para poder ter waves mais pequenas, no caso da esfera default do unity, se ficasse muito pequena, levantava apenas um vértice e ficava um conjunto de pirâmides em vez de uma onda:

![Pyramide Waves](https://cdn.discordapp.com/attachments/1163146681064357908/1192078095595868160/image.png?ex=65a7c43d&is=65954f3d&hm=2d5126176559c3c005c9a072d5b3798736f24e170fb83697cdc99634b45f82d2&)

**Unity Default Sphere:**

![Unity Sphere](https://media.discordapp.net/attachments/1163146681064357908/1191864664804556810/image.png?ex=65a6fd77&is=65948877&hm=39e46189e40ded4f4ff9519dda55b4c0cbe2eae38d982961391124be50aa06a5&=&format=webp&quality=lossless&width=527&height=411)

**ProBuild Sphere:**

![Probuild Sphere](https://media.discordapp.net/attachments/1163146681064357908/1191864665236586578/image.png?ex=65a6fd77&is=65948877&hm=64c33a64b8b70deef139f2cbaf170d4eb9a74c049a19e84b551185c02b2c084c&=&format=webp&quality=lossless&width=562&height=513)

Depois disso adaptei o *shader* a nova esfera e fiquei com este efeito com as 7 ondas (Esfera do ProBuilder à esquerda e Esferda do Unity à direita):

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/18KxEmDjmmkRf2Alndq7GpWrjbjcEhu6O/view?usp=sharing)

Demonstração do efeito com rotação: [Vídeo](https://drive.google.com/file/d/1Kz_kpQA-vv305fSEpCzc_p0d8Rl1MWBe/view?usp=sharing)

**Efeito escudo:**

Em seguida decidi que já estava farto dos tijolos, então comecei a trabalhar para fazer um efeito para o escudo, primeiro deparei-me com este tutorial:

[Unity *Shader* Graph - Sci-Fi Barrier / Shield Tutorial](https://www.youtube.com/watch?v=rB4YMQmO8Mw&t=3s&ab_channel=GabrielAguiarProd.) - Repliquei o tutorial e percebi melhor como funcionava o ruído e os padrões procedimentais. 

Depois de muitos testes e exprimentações criei o seguinte *shader*:

![Cell *Shader*](https://media.discordapp.net/attachments/267705945377734667/1191853749472350268/image.png?ex=65a6f34d&is=65947e4d&hm=020ae28a6005194543573027b3eefe78a37411626dbeadee6c2ae117a0d80a87&=&format=webp&quality=lossless&width=1193&height=676)

Criei um node com o padrão Truchet, que recebe duas variaveis que apenas controlam o *tiling* e complexidade do padrão, depois multiplico este padrão por uma cor para obter a cor do escudo:

![Turchet](https://media.discordapp.net/attachments/1163146681064357908/1192096843610468453/image.png?ex=65a7d5b3&is=659560b3&hm=7ef0966c88d1798885085d68844bee10e8c81b0c9b484a1892880e31be8319bf&=&format=webp&quality=lossless&width=1053&height=670)

Depois criei um node de ruído Voronoi, que recebe a variavel CellDensity que controla quantas celulas o ruído tem.
Depois temos duas variaveis:
- NoiseRotationSpeed: controla quão rapido o ruído se mexe no eixo do X ao longo do tempo;
- CellMovementSpeed: controla a velocidade ao longo do tempo da rotação que as celulas fazem em sua volta.

![Cell Noise](https://media.discordapp.net/attachments/1163146681064357908/1192097362596864161/image.png?ex=65a7d62f&is=6595612f&hm=ca0d91428ad30ccb93ed32258a38ec53a587510758f60be8a91d08a9bba0f520&=&format=webp&quality=lossless&width=1440&height=583)

Por fim multiplico o padrão colorido pelo ruído e fico com o *shader* final:

![Final Noise](https://media.discordapp.net/attachments/1163146681064357908/1192098174509252658/image.png?ex=65a7d6f0&is=659561f0&hm=116d3b0490a57b259853bfb2c0825593449289401731287af39d8322c25db069&=&format=webp&quality=lossless&width=626&height=670)

Demonstração do efeito: [Vídeo](https://drive.google.com/file/d/1ffH2rfUQuvcGpqv0FONTx9NSO0DAW4sH/view?usp=sharing)

**Cor na onda:**

De seguida queria adicionar uma cor à onda, assim para além da deformação fisica, dava uma certa "deformação" da cor.

Para isto multipliquei as ondas recebidas por uma cor que pode ser definida pelo utilizador, que por usa vez pode ser multiplicada por uma itensidade para que esta cor seja mais distinguível, e depois adiciono esta cor ao efeito previamente criado:

![Final color](https://media.discordapp.net/attachments/1163146681064357908/1192099765282951289/image.png?ex=65a7d86c&is=6595636c&hm=45a7127bdcc4f320d311ab8963348d7ca7c5ff4eca8dabb9fb6565f05aca76db&=&format=webp&quality=lossless&width=1026&height=670)

**Intersecção:**

Com isto feito, queria tentar fazer um efeito de intersecção da esfera com objetos na cena. 
Para isso encontrei este video:

[Unity *Shader* Graph - Intersection Effect Tutorial](https://www.youtube.com/watch?v=Uyw5yBFEoXo&t=260s&ab_channel=GabrielAguiarProd.)

Resultado após o tutorial:

![Intersection *Shader*](https://media.discordapp.net/attachments/1163146681064357908/1192159520517013624/image.png?ex=65a81012&is=65959b12&hm=e83f4779d99980d5ad1b72d64b7542ff233187e377d9404a5db72d1902c9f7a5&=&format=webp&quality=lossless&width=531&height=305)

Apesar de conseguir replicar o efeito no video, não consegui aplicá-lo ao meu *shader*, decidi então deixar aqui como *honorable mention* e repliquei o efeito pretendido com duas esferas, uma com o *shader* de intersecão e outra com o *shader* de ondas:

Efeito com 2 esferas: [Vídeo](https://drive.google.com/file/d/10IhK5XWkaiuZDg2ug4eO6ce0A4i8IBWy/view?usp=sharing)

No dia seguinte, por sorte, apareceu-me este vídeo, que mostrava um "museu" de *shaders*, e neste vídeo tinha o *shader* de intersecção outra vez, mas que funcionava de outra maneira: [10 *Shaders* in 10 Minutes - Unity *Shader* Graph](https://www.youtube.com/watch?v=vje0x1BNpp8&t=264s&ab_channel=DanielIlett)

Com este vídeo, consegui finalmente fazer a intersecção no meu *shader*, ja que este recebia a *base color*, e aplicava o efeito por cima da *base color*:

![Intersection *shader*](https://media.discordapp.net/attachments/1163146681064357908/1192444681775239229/image.png?ex=65a919a6&is=6596a4a6&hm=6ee4954967fee55e4b857f3f630ea27e675c0fb4334936fcb15d2901889a1a4c&=&format=webp&quality=lossless&width=1440&height=590)

Primeiro comparamos a profundidade da cena, com o node *scene depth* este node dá-nos a profundidade de cada objeto que esta a ser renderizado, e usando a sample *eye* vemos esta profundidade da prespetiva da câmara.

Depois removemos a este valor a posição do objeto com o node *screen position*, e ficamos com um "buraco" na prespetiva da câmara, depois ao invertermos isto com um *one minus* node ficamos apenas com este "buraco" ficando com a posição do objeto.

A variável IntersectionDepth, permite regular o tamanho da nossa interseção, usamos o node *remap*, que faz a nossa escala que seria de 0 a 1, passar a ser de 1 a 0, para que faça mais sentido, pois assim ao aumentar o valor da variavel, o tamanho da interseção aumenta:

![*Shader* intersection 1](https://media.discordapp.net/attachments/1163146681064357908/1192447101792165938/image.png?ex=65a91be7&is=6596a6e7&hm=b832a4a3151820cb433be4e650e51db35625034bde10cea15bafbbbf5c238ab5&=&format=webp&quality=lossless&width=1440&height=580)

Tudo até este ponto tinha visto no vídeo anterior, é neste novo vídeo que foi introduzida a diferença, ao invês de receber o valor *alpha* da cor e o inserir no campo *alpha* do nosso *shader*, que removia a cor do resto do escudo (o que fazia o efeito não funcionar, pois removia o escudo por inteiro), este *shader* faz um *lerp*, ou seja, onde for transparente, fica com a cor do escudo, caso não seja, fica a cor da interseção.

A variavel IntersectionStrenght permite-nos controlar o node *power* que nos dá o valor elevado a esta IntersectionStrength, que nos dá mais controlo sobre a intensidade do escudo:

![*Shader* intersection 2](https://media.discordapp.net/attachments/1163146681064357908/1192448451980574750/image.png?ex=65a91d29&is=6596a829&hm=e30dcff84517b6b84508d16b57f42b0e8e2418d3d900f256c4c077101f5b2afe&=&format=webp&quality=lossless&width=1347&height=606)

Por fim, o ruído e cor da intersecção, este ruído é extremamente parecido com o ruído do escudo em si em termos de lógica, então não acho que precise de grande explicação, tem apenas algumas variaveis para controlar quão intenso é o movimento, e quão denso o ruído é, depois, como tinha referido, tudo isto é dado a um *lerp*, que faz então a interpolação como referido anteriormente:

![*Shader* intersection 3](https://media.discordapp.net/attachments/1163146681064357908/1192449163468750958/image.png?ex=65a91dd3&is=6596a8d3&hm=a0ea691d9be3193ccd6be9f9ee095e7ddfdd80d7e12f054dca749c998c1aedd2&=&format=webp&quality=lossless&width=1440&height=506)

Efeito com a intersecção final: [Vídeo](https://drive.google.com/file/d/1lMxKZ_Gze4OqLL7l4T1YYEx36WJryL9M/view?usp=sharing)

O único ponto  negativo é que com este *shader*, a cor do escudo é substituida pela da intersecção, ao invés de ficar por cima:

**Efeito antigo:**

![Old Intersection](https://cdn.discordapp.com/attachments/1163146681064357908/1192443653147344927/image.png?ex=65a918b1&is=6596a3b1&hm=cd2cf04e80ccc63a41c83077cc231dc046800f7df3cc2aafb8e4438601ba44d1&)

**Novo efeito:**

![New Intersection](https://media.discordapp.net/attachments/1163146681064357908/1192443922430046299/image.png?ex=65a918f1&is=6596a3f1&hm=49da5ae208590303f744b720d2470935ba03eee7f803f7939f943eac79d479ee&=&format=webp&quality=lossless)

Para terminar fiz uma demo scene a mostar o *shader* em diferentes cenários e diferentes padrões e efeitos que se consegues com o *shader*:

![Demo scene](https://media.discordapp.net/attachments/1163146681064357908/1192490809065480313/image.png?ex=65a9449c&is=6596cf9c&hm=75477fc149d55c00f889995eb0e6cc58e90f58d2c8930e11794b1742124ba831&=&format=webp&quality=lossless&width=753&height=670)

*Shader* final com o nome ShieldCollisionEffect:

![Final *shader*](https://media.discordapp.net/attachments/1163146681064357908/1192491727995547778/image.png?ex=65a94577&is=6596d077&hm=477912de7a16f1910ff2e135ef32c47f4f72e842b0ffd9380fd71bb3d1b600e2&=&format=webp&quality=lossless&width=1418&height=670)

**Fazer o *shader* funcionar com qualquer *mesh*:**

Até este ponto achava que estava concluído e estava a acabar o relatório, mas estava determinado a perceber porque não funcionava em *mesh*'s diferentes de esferas, em cubos funcionava, mas mal.

Inicialmente achei que era porque os cubos têm apenas 6 vértices, e era verdade que era por isso que a deformação da *mesh* era estranha, porque só deformava nos cantos:

Cubo a deformar apenas nos cantos: [Vídeo](https://drive.google.com/file/d/13AGugJlws0gF5y56HlvQ78bH8QCOatkU/view?usp=sharing)

Depois voltei ao meu *subshader* de impacto e comecei a mexer em alguns valores, e mudei a divisão para 1.5:

![*Shader* division](https://media.discordapp.net/attachments/1163146681064357908/1192504358127026196/image.png?ex=65a9513a&is=6596dc3a&hm=3ba32071d3cf33a3070168d7f8429ba6798fb77a9559c07f73d6ae47975b4c06&=&format=webp&quality=lossless&width=597&height=360)

Depois disto a onda ficou quase perfeita nos cantos do cubo, mas no centro não:

Cubo com onda boa nos cantos: [Vídeo](https://drive.google.com/file/d/186dhD4Vr6KdTmyrwCNo2U7QKLnGrvSOI/view?usp=sharing)

Lembrei-me que nesta face do cubo, temos um quadrado e caso o cubo tivesse 1 de tamanho (que é o caso), estas seriam as medidas:

![Cube size](https://media.discordapp.net/attachments/1163146681064357908/1192505553939865711/image.png?ex=65a95257&is=6596dd57&hm=63e7acd662ba0840ac9d1c23b79b9c91507264b6fceaef6be7627bd1bd153c25&=&format=webp&quality=lossless&width=305&height=288)

Logo nos cantos a distância seria aproximadamente 0.7, e quando dividimos 1 por 1.5 temos 0.67, que é bastante perto de 0.7.

Isto fez-me perceber que o problema poderia estar aí, pensei inicalmente que cada *mesh* precisaria de uma divisão diferente, e que isto funcionaria apenas para esferas porque são as unicas com o mesmo tamanho em todas as direções.

Apesar de achar que normalizar o vetor era necessário para obter a direção, decidi remover este componente, já que esta divisão me estava a dar problemas, e só era necessária por causa da normalização, que no final de contas estava apenas a por os vetores todos com o mesmo tamanho, que não era ideal para qualquer objeto que não uma esfera.

Liguei o *focal point* diretamente a subtração da posição no objeto e agora funcionava em qualquer *mesh*:

Efeito em qualquer *mesh*: [Vídeo](https://drive.google.com/file/d/1oUpm9tlhCR86oAzBHN_4ssLzoyfJ9OZq/view?usp=sharing)

Agora tinha um novo problema: 

Como visto no vídeo anterior a onda não propaga até ao final do objeto, apercebi-me que, de novo, ambos no *shader* de impacto e no *script*, eu limitava a progressão até 1, o que funcionava perfeitamente para esferas, e se quisesse o efeito a funcionar em esferas de tamanho 1, era ideal. 

Mas por exemplo na estátua de cavalo do vídeo anterior com tamanho maior que 1 não iria funcionar.

Esta limitação era feita pelo  node *fraction* que me devolvia sempre os valores decimais da progressão, logo nunca chegaria a maior que 1, e caso o valor da progressão continuasse a subir para além de 1, a onda iria repetir, removi o node *fraction* e aumentei o limite de progressão no *script*:

![Removing fraction node](https://media.discordapp.net/attachments/1163146681064357908/1192511620094644335/image.png?ex=65a957fe&is=6596e2fe&hm=0f7c96f3fa8f3f1be170519c2e3da3db7e96b40b73a6fee98d8287b7599a23f4&=&format=webp&quality=lossless&width=833&height=495)

![Removing progression limit](https://media.discordapp.net/attachments/1163146681064357908/1192511769898389545/image.png?ex=65a95821&is=6596e321&hm=ae588386d6c370eebd0c2419a7954074bbb4f8fc99e13d955702d248b55753e9&=&format=webp&quality=lossless&width=732&height=598)

Com isto finalmente consegui a propagação da onda por toda a *mesh*:

Onda completa em qualquer *mesh*: [Vídeo](https://drive.google.com/file/d/1jHvZ51FCers1wT5oelVwNkzbcFY49qUI/view?usp=sharing)

O problema desta resolução, é que, caso um objeto seja maior que 5 vai dar problemas outra vez, mudei o máximo para 50 no *script*, assumindo que a maior parte dos objetos não precisariam de mais, mas é um valor alterável caso necessário, a partir do *script*.

Para terminar dei a opção ao utilizador de usar uma textura em vez do escudo e ruído feito anteriorimente, adicionando mais duas properidades, um booleano que indica se usamos textura ou não, e uma textura, este booleano é usado como predicado antes de serem dados os valores à *Base Color*, mas mantém as ondas:

![Adding texture node](https://media.discordapp.net/attachments/1163146681064357908/1192520162109304972/image.png?ex=65a95ff2&is=6596eaf2&hm=55c49bfef0e6ebb221c270dc2368f94b5affda48f82152091c15a320693ae428&=&format=webp&quality=lossless&width=1440&height=621)

Resultado final: [Vídeo](https://drive.google.com/file/d/1UvbeTKdh6iq7Aym3Z0Twje2flI3Fhapl/view?usp=sharing)

***Shader* final:**

![Final *shader*](https://media.discordapp.net/attachments/1163146681064357908/1192520969068220648/image.png?ex=65a960b2&is=6596ebb2&hm=b53aab3d4c9e501cbfdcf3fd251bedcc9fe6b4d9fef5dc8aa586c53729757954&=&format=webp&quality=lossless&width=1440&height=662)

Testei ainda com um cubo com mais polígonos para confirmar o problema que referi anteriormente, em que os cubos têm apenas 6 vértices e só deformava nos cantos, enquanto que este cubo tem mais e agora deformaria como deveria:

![Cube with more triangles](https://media.discordapp.net/attachments/1163146681064357908/1192524360083324988/image.png?ex=65a963db&is=6596eedb&hm=f397e1e8ec263892499b3b457dad57f7bbd0759f54765b00671a1e0b321f61d6&=&format=webp&quality=lossless&width=527&height=471)

Cubo com mais vértices: [Vídeo](https://drive.google.com/file/d/1Xxt-5fU70ZuHMxY4fVt_M2x41p362fDg/view?usp=sharing)

**Mudar o ponto default no *script*:**

Enquanto adicionava novas *meshs* à demo scene, apercebi-me de um novo problema.
O ponto default para diferentes *meshs* poderia ser diferente, no caso do plano, caso assumissemos que o *focal point* "nulo" é (0, 0, 0), isto acontecia:

![Plano com pico](https://media.discordapp.net/attachments/1163146681064357908/1192529151584899163/image.png?ex=65a96851&is=6596f351&hm=e42bdc68cfb129da3941ebf7130866f572ad3fadd3ebed9c38bc60edf732ac6a&=&format=webp&quality=lossless&width=713&height=670)

Não arranjei solução correta para este problema, então alterei a posição *default* para um número bastante grande que assumi que muitos poucos modelos teriam, que, mais uma vez, caso fosse necessário, poderia ser alterado no *script*:

![*Script* com default diferente](https://media.discordapp.net/attachments/1163146681064357908/1192530783609552926/image.png?ex=65a969d6&is=6596f4d6&hm=cedb843adf77c770e40fb04723132f200d35d72c99674b9ad1ead913555c8e29&=&format=webp&quality=lossless&width=743&height=353)

**Demo scene final**

![Final demo scene](https://media.discordapp.net/attachments/1163146681064357908/1192533079840014336/image.png?ex=65a96bfa&is=6596f6fa&hm=b0f95300f2255376646c70ff43a76f4bc928949ca026ef5e3de8d3aafccf3956&=&format=webp&quality=lossless&width=1242&height=670)

### **Conclusões finais:**

Durante este trabalho ganhei bastante conhecimento sobre *shaders*, antes sentia que muitas coisas eram "magia" porque não percebia como funcionavam e também nao sabia sequer as possibilidade que um *shader* tem, por serem muitas, depois disto descobri muito do que se pode fazer, e apercebi-me quão extenso é.

Relembrei-me também que não devo tentar perceber as coisas sem as questionar, se tivesse questionado a normalização e o node *fraction* mais cedo teria poupado muita dor de cabeça.

Felizmente consegui obter o *shader* exatamente como queria, e ainda vejo que há muito espaço para expansão, como por exemplo adicionar a opção de textura no final, é possível adicionar muito mais coisas ainda, e penso continuar a trabalhar neste *shader* e em outros, pois acabei por gostar mais de *shaders* do que esperava.

Aprendi também que ao gravar os objetos como prefabs ao invés de os ter apenas na *scene*, reduz imenso o tamanho que a scene ocupa no ficheiro, pois ao invés de gravar os objetos na *scene*, grava-os como ficheiro, o que foi uma grande salvação quando a *scene* tinha mais de 100mb e já estava a usar o git lfs e o github não permitia mais que isto: [Link para a discussão onde descobri isto](https://forum.unity.com/threads/git-and-unity-scene-files-larger-than-100mb.1038838/)


### **Um obrigado adicional a:**

- David Brás: que me ajudou em alguns conceitos iniciais para o *shader*, especialmente a compreender melhor o *Vertext Displacement*;
- João Silva (a22004451): que me fornecer alguns modelos 3D feitos por ele com mais e menos polígonos para testar o *shader*.

### **Bibliografia:**

- [*Shader* Graph Forcefield: Update](https://www.youtube.com/watch?v=P47yMdetoE4&ab_channel=WilmerLinGASchool) por Wilmer Lin GA School (17/07/2020)
- [Energy Shield Effect in Unity URP *Shader* Graph](https://www.youtube.com/watch?v=o4CGL2YXs5k&ab_channel=Imphenzia) por Imphenzia (20/04/2023)
- [Shockwave *Shader* Graph - How to make a shock wave *shader* in Unity URP/HDRP](https://www.youtube.com/watch?v=dFDAwT5iozo&ab_channel=GameDevBill) por GameDevBill (08/12/2020)
- [Unity *Shader* Graph VFX - Bubble Shield (Tutorial)](https://www.youtube.com/watch?v=jdAbVkre8cw&ab_channel=ABitOfGameDev) por A Bit Of Game Dev (08/09/2021)
- [Unity *Shader* Graph - Sci-Fi Barrier / Shield Tutorial](https://www.youtube.com/watch?v=rB4YMQmO8Mw&t=3s&ab_channel=GabrielAguiarProd.) por Gabriel Aguiar Prod. (06/09/2022)
- [Unity *Shader* Graph - Intersection Effect Tutorial](https://www.youtube.com/watch?v=Uyw5yBFEoXo&t=260s&ab_channel=GabrielAguiarProd.) por Gabriel Aguiar Prod. (13/06/2023)
- [10 *Shaders* in 10 Minutes - Unity *Shader* Graph](https://www.youtube.com/watch?v=vje0x1BNpp8&t=264s&ab_channel=DanielIlett) por Daniel Ilett (03/04/2022)
- [Tree Model](https://sketchfab.com/3d-models/tree-c25e5fc2bfa54d24a165a8a207c0a6d7) "Tree" (https://skfb.ly/onxWu) por Epic_Tree_Store está licenciado sob Creative Commons Atribuição (http://creativecommons.org/licenses/by/4.0/).
- [Statue Model](https://sketchfab.com/3d-models/statue-ecd586875a8845cebb74aa9ea11756ed) "Statue" (https://skfb.ly/opuMP) por DJMaesen está licenciado sob Creative Commons Atribuição (http://creativecommons.org/licenses/by/4.0/).