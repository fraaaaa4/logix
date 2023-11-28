/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Copyright (C) 2001 Gerwin Klein <lsf@jflex.de>                          *
 * All rights reserved.                                                    *
 *                                                                         *
 * This is a modified version of the example from                          *
 *   http://www.lincom-asg.com/~rjamison/byacc/                            *
 *                                                                         *
 * Thanks to Larry Bell and Bob Jamison for suggestions and comments.      *
 *                                                                         *
 * License: BSD  j                                                          *
 *                                                                         *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

%{
  import java.io.*;
%}
      
%token NL          /* newline  */
%token <sval> NUM  /* a number */
%token LOG10       /* token per logaritmo */
%token ln          /* token per logaritmo naturale */

%type <sval> exp

/* 'precedenza dell'associatività' */
%left '-' '+'
%left '*' '/' '%'
%left NEG          /* negation--unary minus */
%left LOG10        /* logaritmo in base 10 */
%right '^'         /* exponentiation        */
%left ln           /* logaritmo naturale */
      
%%

input:   /* empty string */
       | input line
       ;
      
       line:    NL      { if (interactive) System.out.print("Expression: "); 
                          }          
       | exp NL  { System.out.println(" = " + $1); 
                   if (interactive) System.out.print("Expression: "); }
       ;
      
exp:     NUM                { $$ = $1; }
       | exp '+' exp        { $$ = $1 + "+" + $3; }
       | exp '-' exp        { $$ = $1 + "-" + $3; }
       | exp '*' exp        { $$ = $1 + "*" + $3; }
       | exp '/' exp        { $$ = $1 + "/" + $3; }
       | exp '%' exp        { $$ = $1 + "%" + $3; }
       | '-' exp  %prec NEG { $$ = "-" + $2; }
       | exp '^' exp        { $$ = $1 + "^" + $3; }
       | '(' exp ')'        { $$ = $2; }
       | LOG10 exp          { $$ = "log10" + $2; }
       | ln exp             { $$ = "ln" + $2;   }
       ;

%%

  private Yylex lexer;


  private int yylex () {
    int yyl_return = -1;
    try {
      yylval = new ParserVal(0);
      yyl_return = lexer.yylex();
    }
    catch (IOException e) {
      System.err.println("IO error :"+e);
    }
    return yyl_return;
  }


  public void yyerror (String error) {
    System.err.println ("Error: " + error);
  }


  public Parser(Reader r) {
    lexer = new Yylex(r, this);
  }


  static boolean interactive;

  public static void main(String args[]) throws IOException {
    System.out.println("BYACC/Java with JFlex Calculator Demo");

    Parser yyparser;
    if ( args.length > 0 ) {
      // parse a file
      yyparser = new Parser(new FileReader(args[0]));
    }
    else {
      // interactive mode
      System.out.println("[Quit with CTRL-D]");
      System.out.print("Expression: ");
      interactive = true;
        yyparser = new Parser(new InputStreamReader(System.in));
    }

    yyparser.yyparse();
    
    if (interactive) {
      System.out.println();
      System.out.println("Have a nice day");
    }
  }
