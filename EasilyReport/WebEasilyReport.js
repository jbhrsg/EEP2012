function CheckNum()
{
    var objRegex=/[\d]/ig;//���ֵ����t���_ʽ
    if (String.fromCharCode(event.keyCode).match(objRegex) == null && event.keyCode!=9)
    ReturnFalse();
}

function CheckDecimal(el, ev)
{
    //if(isNaN(String.fromCharCode(event.keyCode)))
    //8���˸����46��delete��37-40�� �����
//48-57��С�����������֡�96-105����������������
//110��190��С������������������С��
//189��109��С�����������������ĸ���
    var event = ev || window.event;                             //IE��FF�»�ȡ�¼�����
    var currentKey = event.charCode||event.keyCode;             //IE��FF�»�ȡ������
    
    //С���㴦��
    if (currentKey == 110 || currentKey == 190) {
        if (el.value.indexOf(".")>=0) 
            ReturnFalse();

    } else 
        if (currentKey!=8 && currentKey !=9 && currentKey != 46 && (currentKey<37 || currentKey>40) && (currentKey<48 || currentKey>57) && (currentKey<96 || currentKey>105))
            ReturnFalse();

}

function ReturnFalse()
{
    if (window.event)                       //IE
                event.returnValue=false;                 //e.returnValue = false;Ч����ͬ.
            else                                    //Firefox
                event.preventDefault();
}