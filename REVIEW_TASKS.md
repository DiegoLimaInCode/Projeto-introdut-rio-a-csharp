# Revisão rápida da base de código

Esta revisão identificou alguns pontos de melhoria no código e na documentação atual.

## 1) Tarefa para corrigir erro de digitação
**Problema encontrado:** há strings de interface sem acento em português (por exemplo: `Voce`, `Deposito`, `Operacao`, `invalido`), o que reduz a qualidade da experiência do usuário.

**Tarefa sugerida:**
- Padronizar as mensagens do console para português correto:
  - `Voce` → `Você`
  - `Deposito` → `Depósito`
  - `Operacao` → `Operação`
  - `invalido` → `inválido`

**Critério de aceite:** todas as mensagens exibidas ao usuário devem seguir ortografia e acentuação corretas em português-BR.

---

## 2) Tarefa para corrigir bug
**Problema encontrado:** o número da conta (`accountNumber`) é gerado, mas nunca é usado nem validado para unicidade. Além disso, o intervalo `1..1000` pode gerar colisões com facilidade.

**Tarefa sugerida:**
- Expor o número da conta de forma segura (somente leitura) e implementar geração com garantia de unicidade (ex.: sequência incremental estática ou GUID com máscara amigável).
- Adicionar validação para impedir contas duplicadas.

**Critério de aceite:** criação de múltiplas contas em execução contínua não deve gerar números duplicados; número da conta deve estar disponível para exibição/diagnóstico.

---

## 3) Tarefa para ajustar discrepância de documentação
**Problema encontrado:** o `README.md` afirma que o projeto cobre tópicos que não aparecem na implementação atual (ex.: `switch`, laços `for/while/foreach`, arrays e coleções).

**Tarefa sugerida:**
- Atualizar a seção **Conceitos Abordados** para refletir o que realmente existe hoje no código.
- Opcionalmente, criar um roadmap com os tópicos ainda não implementados para evitar ambiguidade.

**Critério de aceite:** lista de conceitos no `README.md` deve corresponder ao conteúdo atual do código, separando claramente "implementado" de "planejado".

---

## 4) Tarefa para melhorar teste
**Problema encontrado:** não há projeto de testes automatizados; regras de negócio de `Deposit` e `Withdraw` não estão protegidas por testes.

**Tarefa sugerida:**
- Criar projeto de teste (xUnit/NUnit) com cenários mínimos:
  - depósito com valor positivo aumenta saldo;
  - depósito com valor zero/negativo lança exceção;
  - saque com valor positivo e saldo suficiente reduz saldo;
  - saque com saldo insuficiente lança exceção;
  - saque com valor zero/negativo lança exceção.

**Critério de aceite:** suíte de testes executa em `dotnet test` e cobre os principais fluxos de sucesso e erro de `BankAccount`.
