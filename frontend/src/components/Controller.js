import Context from '../context/Context.js';

export default new class Controller{

    hideModal(){
        Context.cursor.set('formToggle',false);
        Context.cursor.set('model',null);
        console.log("Hidden");
    }
}