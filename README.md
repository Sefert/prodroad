# icd0006-21-22-s
#Name:Marko Linde, uni-ID:marko.moznikov code:176292IADB

COMMANDS:
npm init
npm install --save-dev html-webpack-plugin webpack webpack-cli webpack-dev-server
npm install --save-dev style-loader css-loader
npx webpack serve --mode development

ADMINISTRATIVE HELPERS:
https://www.codegrepper.com/code-examples/shell/Error%3A+EACCES%3A+permission+denied%2C+open+mac
mkdir ~/.npm-global
npm config set prefix '~/.npm-global'
cd ~/.profile  or touch ~/.profile and add export PATH=~/.npm-global/bin:$PATH
source ~/.profile

HELP:
<script src="test.js" type="text/javascript"></script>
undo last commit: git reset --soft HEAD~1