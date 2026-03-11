export interface RelatorioPessoa {
  pessoa: string
  receitas: number
  despesas: number
  saldo: number
}

export interface RelatorioCategoria {
  categoria: string
  receitas: number
  despesas: number
  saldo: number
}

export interface RelatorioData {
  pessoas: RelatorioPessoa[]
  categorias: RelatorioCategoria[]
  totalReceitas: number
  totalDespesas: number
  saldoLiquido: number
}