"makecert_x64" -n  "CN=kids.bmi.ir"    -a sha1 -sky  exchange  -pe -sv "kids.pvk"  "kids.cer"
"pvk2pfx"  -pvk "kids.pvk" -spc  "kids.cer" -pfx "kids.pfx" -pi  @kids@