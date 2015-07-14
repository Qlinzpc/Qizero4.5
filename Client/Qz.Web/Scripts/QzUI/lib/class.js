//所有类的基类
var Class = function () { };
//基类增加一个extend方法
Class.extend = function (prop) {
    var _super = this.prototype;
    //父类的实例赋给变量prototype
    var prototype = new this();
    //把要扩展的属性复制到prototype变量上
    for (var name in prop) {
        //下面代码是让ctor里可以直接访问使用this._super访问父类构造函数，除了ctor的其他方法，this._super都是访问父类的实例
        prototype[name] = name == "ctor" && typeof prop[name] == "function" &&
          typeof _super[name] == "function" ?
          (function (name, fn) {
              return function () {
                  //备份一下this._super
                  var tmp = this._super;
                  //替换成父类的同名ctor方法
                  this._super = _super[name];
                  //执行，此时fn中的this里面的this._super已经换成了_super[name],即父类的同名方法
                  var ret = fn.apply(this, arguments);
                  //把备份的还原回去
                  this._super = tmp;
                  return ret;
              };
          })(name, prop[name]) :
          prop[name];
    }
    //假的构造函数
    function Class() {
        //执行真正的ctor构造函数
        this.ctor.apply(this, arguments);
    }
    //继承父类的静态属性
    for (var key in this) {
        if (this.hasOwnProperty(key) && key != "extend")
            Class[key] = this[key];
    }
    // 子类的原型指向父类的实例
    Class.prototype = prototype;
    //这里一定要用new this
    //不能Class.prototype._super = prototype;（这里明显错误，prototype这时已经被copy进去了新的属性）
    //或者Class.prototype._super = _super;（这里会导致_super instanceof 不准确 ）
    Class.prototype._super = new this();
    //覆盖父类的静态属性
    if (prop.statics) {
        for (var name in prop.statics) {
            if (prop.statics.hasOwnProperty(name)) {
                Class[name] = prop.statics[name];
                if (name == "ctor") {
                    Class[name]();
                }
            }
        }
    }
    Class.prototype.constructor = Class;
    //原型可扩展
    Class.extendPrototype = function (prop) {
        for (var name in prop) {
            prototype[name] = prop[name];
        }
    };
    //任何Class.extend的返回对象都将具备extend方法
    Class.extend = arguments.callee;
    return Class;
};


var Animal = Class.extend({
    statics: {
        TestStaticsProperty: 1,
        TestStaticsProperty2: 2,
        TestStaticsMethod: function () {
            return 2;
        },
        TestStaticsMethod2: function () {
            return 33;
        }
    },
    ctor: function (age) {
        this.age = age;
        this.testProp = "animal";
    },
    eat: function () {
        return "nice";
    },
    dirnk: function () {
        return "good";
    }
})
var Pig = Animal.extend(
{
    statics: {
        TestStaticsProperty2: 3,
        TestStaticsMethod2: function () {
            return 3;
        }
    },
    ctor: function (age, name) {
        this._super(age);
        this.name = name;
    },
    climbTree: function () {
        return this._super.eat();
    },
    eat: function () {
        return "very nice"
    }
});
var BigPig = Pig.extend({
    ctor: function () {
        //测试通过this._super访问父类构造函数
        console.log(typeof this._super === "function");
    },
    getSuper: function () {
        return this._super;
    }
})
//测试静态属性
console.log(Animal.TestStaticsProperty === 1);//true
//测试静态方法
console.log(Animal.TestStaticsMethod() === 2);//true
//测试子类访问父类静态属性
console.log(Pig.TestStaticsProperty === 1);//true
//测试子类重写父类静态属性
console.log(Pig.TestStaticsProperty2 === 3);//true
//测试子类访问父类静态属性
console.log(Pig.TestStaticsMethod() === 2);//true
//测试子类重写父类静态属性
console.log(Pig.TestStaticsMethod2() === 3);//true
//测试父类静态方法未被覆盖
console.log(Animal.TestStaticsMethod2() === 33);//true
//测试父类静态属性未被覆盖
console.log(Animal.TestStaticsProperty2 === 2);//true
var animal = new Animal(11);
//测试构造函数 
console.log(animal.age === 11);//true
console.log(animal.eat() === "nice");//true
var pig = new Pig(1, 11);//true
console.log(pig.testProp === "animal");//true
console.log(pig.climbTree() === "nice");//true
console.log(pig.eat() === "very nice");//true
console.log(pig instanceof Pig)//true
var bigPig = new BigPig();
//测试孙类访问祖先方法（原型链）
console.log(bigPig.dirnk() === "good");//true
//测试孙类访问祖先属性
console.log(bigPig.testProp === "animal");//true	 
console.log(bigPig instanceof BigPig);//true
//测试通过this._super访问父类
console.log(bigPig.getSuper() instanceof Pig);//true
console.log(bigPig instanceof Animal);//true
