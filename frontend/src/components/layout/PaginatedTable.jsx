import React from 'react';

export default class PaginatedTable extends React.Component {
    constructor(){
        super();

        this.state = {
            numberInput: ''
        };
    }

    onDropdownChange(e){
        this.setState({
            numberInput: ''
        });
        this.props.changeHandler(e.target.value, 1);
    }
    
    onButtonClick(button){
        console.log('some button pressed!!!',button);
        this.setState({
            numberInput: ''
        });
        const { totalCount, pageSize, selectedPage, changeHandler} = this.props;

        switch(button){
            case 'first': 
                changeHandler(+pageSize,1);
                break;
            case 'previous': 
                changeHandler(+pageSize, +selectedPage-1);
                break;
            case 'next': 
                changeHandler(+pageSize, +selectedPage+1);
                break;
            case 'last': 
                const nrOfPages = Math.floor((+totalCount + (+pageSize-1)) / +pageSize);
                changeHandler(+pageSize, +nrOfPages);
                break;
        }
    }

    onNumberInputChange(e){
        const val = e.target.value.trim();
        const { totalCount, pageSize} = this.props;
        const nrOfPages = Math.floor((+totalCount + (+pageSize-1)) / +pageSize);

        if(val === '' || Number.isInteger(+val) && val >= 1 && val <= nrOfPages){
            this.setState({
                numberInput: val
            });
        }
    }

    onNumberInputKeyUp(e){
        if(e.keyCode === 13 && this.state.numberInput != ''){  //<Enter>
            this.props.changeHandler(+this.props.pageSize, +this.state.numberInput);
        }
        else if(e.keyCode === 27){ //<Esc>
            this.setState({
                numberInput: ''
            });
        }
    }

    
    render(){
        const {
            header, 
            listOfItems, 
            totalCount, 
            pageSize, 
            selectedPage
        } = this.props;

        const nrOfPages = Math.floor((+totalCount + (+pageSize-1)) / +pageSize);

        return (
            <div>
                <table className="pagtable table table-hover table-bordered table-responsive">
                    {header}
                    <tbody>
                    {listOfItems}
                    </tbody>
                </table>
                
                <div>
                    <div className="col-md-4">
                        <label>Page size:</label>&nbsp;&nbsp;
                        <select className="dropdown-toggle" value={pageSize} onChange={this.onDropdownChange.bind(this)}>
                            <option value="5">5</option>
                            <option value="10">10</option>
                            <option value="20">20</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                    </div>
                    <div className="col-md-8 pull-right text-right">
                        <span>{selectedPage} of {nrOfPages}</span>&nbsp;&nbsp;&nbsp;
                        <button type="button" className="btn" disabled={selectedPage <= 1}
                            onClick={this.onButtonClick.bind(this,'first')}    
                        ><strong>&lt;&lt;</strong></button>
                        <button type="button" className="btn" disabled={selectedPage <= 1}
                            onClick={this.onButtonClick.bind(this,'previous')}    
                        ><strong>&lt;</strong></button>
                        <button type="button" className="btn" disabled={selectedPage >= nrOfPages}
                            onClick={this.onButtonClick.bind(this,'next')}    
                        ><strong>&gt;</strong></button>
                        <button type="button" className="btn" disabled={selectedPage >= nrOfPages}
                            onClick={this.onButtonClick.bind(this,'last')}    
                        ><strong>&gt;&gt;</strong></button>
                        <span>Go to page: </span>
                        <input className="text-right" type="text" size="1" 
                            value={this.state.numberInput}
                            onChange={this.onNumberInputChange.bind(this)}
                            onKeyUp={this.onNumberInputKeyUp.bind(this)}
                        />
                    </div>
                </div>
            </div>
        );
    }
}